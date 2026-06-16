using FluentAssertions;
using Moq;
using TransactionWebhook.Application;
using TransactionWebhook.Application.DTO;
using TransactionWebhook.Application.Service;
using TransactionWebhook.Domain.Entities;

namespace TransactionWebhook.Tests;

public class UnitTest1
{
    
    [Fact]
    public async Task Calculates_Fee_Correctly()
    {
        var transactionRepo =
            new Mock<ITransactionRepository>();

        transactionRepo
            .Setup(x => x.ExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        var derivedRepo =
            new Mock<IDerivedRecordRepository>();

        DerivedRecord? captured = null;

        derivedRepo
            .Setup(x => x.AddAsync(It.IsAny<DerivedRecord>()))
            .Callback<DerivedRecord>(x => captured = x);

        var service = new TransactionService(
            transactionRepo.Object,
            derivedRepo.Object);

        await service.ProcessAsync(
            new TransactionRequest(
                "TXN001",
                1000m,
                "NGN"));

        captured!.Fee.Should().Be(20m);
        captured.NetAmount.Should().Be(980m);
    }

    [Fact]
    public async Task ProcessAsync_Should_Throw_Exception_When_Transaction_Already_Exists()
    {
        // Arrange
        var transactionRepository = new Mock<ITransactionRepository>();

        transactionRepository
            .Setup(x => x.ExistsAsync("TXN001"))
            .ReturnsAsync(true);

        var derivedRecordRepository = new Mock<IDerivedRecordRepository>();

        var service = new TransactionService(
            transactionRepository.Object,
            derivedRecordRepository.Object);

        var request = new TransactionRequest(
            "TXN001",
            1000m,
            "NGN");

        // Act
        Func<Task> act = async () =>
            await service.ProcessAsync(request);

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(act);

        Assert.Equal(
            "Transaction ID 'TXN001' already exists.",
            exception.Message);

        transactionRepository.Verify(
            x => x.AddAsync(It.IsAny<Transaction>()),
            Times.Never);

        derivedRecordRepository.Verify(
            x => x.AddAsync(It.IsAny<DerivedRecord>()),
            Times.Never);
    }

    [Fact]
    public async Task ProcessAsync_Should_Throw_Exception_When_TransactionId_Is_Empty()
    {
        // Arrange
        var transactionRepository = new Mock<ITransactionRepository>();
        var derivedRecordRepository = new Mock<IDerivedRecordRepository>();

        var service = new TransactionService(
            transactionRepository.Object,
            derivedRecordRepository.Object);

        var request = new TransactionRequest(
            "",
            1000m,
            "NGN");

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.ProcessAsync(request));

        // Assert
        Assert.Equal(
            "Transaction ID is required.",
            exception.Message);
    }

    [Fact]
    public async Task ProcessAsync_Should_Throw_Exception_When_Currency_Is_Empty()
    {
        // Arrange
        var transactionRepository = new Mock<ITransactionRepository>();
        var derivedRecordRepository = new Mock<IDerivedRecordRepository>();

        var service = new TransactionService(
            transactionRepository.Object,
            derivedRecordRepository.Object);

        var request = new TransactionRequest(
            "TXN001",
            1000m,
            "");

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.ProcessAsync(request));

        // Assert
        Assert.Equal(
            "Currency is required.",
            exception.Message);
    }

    [Fact]
    public async Task ProcessAsync_Should_Throw_Exception_When_Amount_Is_Zero()
    {
        // Arrange
        var transactionRepository = new Mock<ITransactionRepository>();
        var derivedRecordRepository = new Mock<IDerivedRecordRepository>();

        var service = new TransactionService(
            transactionRepository.Object,
            derivedRecordRepository.Object);

        var request = new TransactionRequest(
            "TXN001",
            0m,
            "NGN");

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.ProcessAsync(request));

        // Assert
        Assert.Equal(
            "Amount must be greater than zero.",
            exception.Message);
    }
}
