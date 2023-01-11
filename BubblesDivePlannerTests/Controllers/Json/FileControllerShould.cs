using System.Collections.Generic;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Json
{
    public class FileControllerShould
    {
        private readonly Mock<IJsonController> jsonController = new();
        private readonly Mock<IFilePresenter> filePresenter = new();
        private readonly IDivePlan divePlan;
        private readonly List<IDivePlan> expectedDivePlans = new();
        private IFileController fileController;

        public FileControllerShould()
        {
            divePlan = new DivePlan(TestFixture.ExpectedDiveModel, TestFixture.ExpectedCylinders(), TestFixture.FixtureDiveStep, TestFixture.ExpectedSelectedCylinder);
            expectedDivePlans.Add(divePlan);
            expectedDivePlans.Add(divePlan);
        }

        [Fact]
        public void SaveFile()
        {
            // Given
            jsonController.Setup(jc => jc.Serialise(expectedDivePlans));
            filePresenter.Setup(p => p.DisplaySaveFile()).Returns(true);
            fileController = new FileController(filePresenter.Object, jsonController.Object, expectedDivePlans);
            fileController.AddDivePlan(divePlan);
            fileController.AddDivePlan(divePlan);

            // When
            fileController.SaveFile();

            // Then
            filePresenter.VerifyAll();
            jsonController.VerifyAll();
        }

        [Fact]
        public void DenySaveFile()
        {
            // Given
            jsonController.Setup(jc => jc.Serialise(expectedDivePlans));
            filePresenter.Setup(p => p.DisplaySaveFile()).Returns(false);
            fileController = new FileController(filePresenter.Object, jsonController.Object, expectedDivePlans);

            // When
            fileController.SaveFile();

            // Then
            filePresenter.VerifyAll();
            jsonController.Verify(jc => jc.Serialise(expectedDivePlans), Times.Never);
        }

        [Fact]
        public void LoadFile()
        {
            // Given
            filePresenter.Setup(p => p.DisplayLoadFile()).Returns(false);
            fileController = new FileController(filePresenter.Object, jsonController.Object, expectedDivePlans);

            // When
            fileController.LoadFile();

            // Then
            filePresenter.VerifyAll();
        }

        [Fact]
        public void Intergrate()
        {
            // Given
            var expectedDivePlan = new DivePlan(TestFixture.FixtureDiveModel(null), TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            filePresenter.Setup(p => p.DisplaySaveFile()).Returns(true);
            filePresenter.Setup(p => p.DisplayLoadFile()).Returns(true);
            fileController = new FileController(filePresenter.Object, new JsonController(), new());
            fileController.AddDivePlan(divePlan);
            fileController.AddDivePlan(divePlan);

            // When
            fileController.AddDivePlan(expectedDivePlan);
            fileController.AddDivePlan(expectedDivePlan);
            fileController.SaveFile();
            var actualDivePlan = fileController.LoadFile();

            // Then
            filePresenter.VerifyAll();
            Assert.Equivalent(expectedDivePlan.DiveModel, actualDivePlan.DiveModel);

            for (int i = 0; i < expectedDivePlan.Cylinders.Count; i++)
            {
                Assert.Equivalent(expectedDivePlan.Cylinders[i], actualDivePlan.Cylinders[i]);
            }
        }

        [Fact]
        public void IntergrateDenyLoad()
        {
            // Given
            var expectedDivePlan = new DivePlan(TestFixture.FixtureDiveModel(null), TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            filePresenter.Setup(p => p.DisplaySaveFile()).Returns(true);
            filePresenter.Setup(p => p.DisplayLoadFile()).Returns(false);
            fileController = new FileController(filePresenter.Object, new JsonController(), new());
            fileController.AddDivePlan(divePlan);
            fileController.AddDivePlan(divePlan);

            // When
            fileController.AddDivePlan(expectedDivePlan);
            fileController.AddDivePlan(expectedDivePlan);

            fileController.SaveFile();
            var actualDivePlan = fileController.LoadFile();

            // Then
            filePresenter.VerifyAll();
            Assert.Null(actualDivePlan);
        }
    }
}