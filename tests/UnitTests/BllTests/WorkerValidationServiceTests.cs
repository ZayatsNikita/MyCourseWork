using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using DL.Entities;
using BL.Exceptions;
using FluentAssertions;
using BL.Services.Validaton;
using BL.DtoModels;
using DL.Repositories.Abstract;
using DL.Repositories.Realization.MongoDbRepostories;

namespace UnitTests.BllTests
{
    [TestFixture]
    public class WorkerValidationServiceTests
    {
        public const int TestPasswordNumber = 1;

        public const string TestPersonalData = "Kiril Dmitriev";

        [Test]
        public void CheckForValidyToCreate_WhenThereIsWorkerWithSpecifiedIdInStorage_ShouldReturnValidationException()
        {
            // Arrange
            var workers = new List<WorkerEntity>()
            {
                new WorkerEntity { PassportNumber = TestPasswordNumber, PersonalData = TestPersonalData  }
            };

            var fakeWorkerRepository = new FakeWorkerRepository(workers);

            var validator = new WorkerValidationService(fakeWorkerRepository);

            var workerForChecking = new Worker { PassportNumber = TestPasswordNumber, PersonalData = "some data" };

            // Act
            Action operation = () => 
            {
                validator.CheckForValidyToCreate(workerForChecking);
            };

            // Assert
            operation.Should().Throw<ValidationException>()
                .WithMessage(Messages.ExsistingWorkerId);
        }

        [Test]
        public void CheckForValidyToCreate_WhenIdIsCorrect_TheExceptionShouldNotHaveBeenReturned()
        {
            // Arrange
            var workers = new List<WorkerEntity>()
            {
                new WorkerEntity { PassportNumber = TestPasswordNumber, PersonalData = TestPersonalData  }
            };

            var fakeWorkerRepository = new FakeWorkerRepository(workers);

            var validator = new WorkerValidationService(fakeWorkerRepository);

            var workerForChecking = new Worker { PassportNumber = TestPasswordNumber + 1, PersonalData = "some data" };

            // Act
            Action operation = () =>
            {
                validator.CheckForValidyToCreate(workerForChecking);
            };

            // Assert
            operation.Should().NotThrow<ValidationException>();
        }

        [TestCase(2)]
        [TestCase(101)]
        public void CheckForValidyToCreate_WhenPersonalDataHasIncorectLength_ShouldReturnValidationException(int personalDataLength)
        {
            // Arrange
            var fakeWorkerRepository = new FakeWorkerRepository(new List<WorkerEntity>());

            var validator = new WorkerValidationService(fakeWorkerRepository);

            var workerForChecking = new Worker { PassportNumber = TestPasswordNumber, PersonalData = new string('o', personalDataLength) };

            // Act
            Action operation = () =>
            {
                validator.CheckForValidyToCreate(workerForChecking);
            };

            // Assert
            operation.Should().Throw<ValidationException>()
                .WithMessage(Messages.WrongFIOLength);
        }

        [TestCase(3)]
        [TestCase(50)]
        [TestCase(100)]
        public void CheckForValidyToCreate_WhenPersonalDataHasCorectLength_TheExceptionShouldNotHaveBeenReturned(int personalDataLength)
        {
            // Arrange
            var fakeWorkerRepository = new FakeWorkerRepository(new List<WorkerEntity>());

            var validator = new WorkerValidationService(fakeWorkerRepository);

            var workerForChecking = new Worker { PassportNumber = TestPasswordNumber, PersonalData = new string('o', personalDataLength) };

            // Act
            Action operation = () =>
            {
                validator.CheckForValidyToCreate(workerForChecking);
            };

            // Assert
            operation.Should().NotThrow<ValidationException>();
        }
    }
}
