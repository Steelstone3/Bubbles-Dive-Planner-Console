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
        private readonly Mock<IPresenter> presenter = new();
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
            presenter.Setup(p => p.GetConfirmation("Save File?")).Returns(true);
            fileController = new FileController(presenter.Object, jsonController.Object, expectedDivePlans);
            fileController.AddDivePlan(divePlan);
            fileController.AddDivePlan(divePlan);

            // When
            fileController.SaveFile();

            // Then
            presenter.VerifyAll();
            jsonController.VerifyAll();
        }

        [Fact]
        public void DenySaveFile()
        {
            // Given
            jsonController.Setup(jc => jc.Serialise(expectedDivePlans));
            presenter.Setup(p => p.GetConfirmation("Save File?")).Returns(false);
            fileController = new FileController(presenter.Object, jsonController.Object, expectedDivePlans);

            // When
            fileController.SaveFile();

            // Then
            presenter.VerifyAll();
            jsonController.Verify(jc => jc.Serialise(expectedDivePlans), Times.Never);
        }

        [Fact]
        public void LoadFile()
        {
            // Given
            presenter.Setup(p => p.GetConfirmation("Load File?")).Returns(true);
            fileController = new FileController(presenter.Object, jsonController.Object, expectedDivePlans);

            // When
            fileController.LoadFile();

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void DenyLoadFile()
        {
            // Given
            jsonController.Setup(jc => jc.Deserialise(string.Empty));
            presenter.Setup(p => p.GetConfirmation("Load File?")).Returns(false);
            fileController = new FileController(presenter.Object, jsonController.Object, expectedDivePlans);

            // When
            fileController.LoadFile();

            // Then
            presenter.VerifyAll();
            jsonController.Verify(jc => jc.Deserialise(""), Times.Never);
        }

        [Fact]
        public void IntergrationTest()
        {
            // Given
            var expectedDivePlan = new DivePlan(TestFixture.FixtureDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            presenter.Setup(p => p.GetConfirmation("Save File?")).Returns(true);
            presenter.Setup(p => p.GetConfirmation("Load File?")).Returns(true);
            fileController = new FileController(presenter.Object, new JsonController(), new());
            fileController.AddDivePlan(divePlan);
            fileController.AddDivePlan(divePlan);

            // When
            fileController.AddDivePlan(expectedDivePlan);
            fileController.AddDivePlan(expectedDivePlan);
            fileController.SaveFile();
            var actualDivePlan = fileController.LoadFile();

            // Then
            presenter.VerifyAll();
            Assert.Equivalent(expectedDivePlan.DiveModel, actualDivePlan.DiveModel);

            for (int i = 0; i < expectedDivePlan.Cylinders.Count; i++)
            {
                Assert.Equivalent(expectedDivePlan.Cylinders[i], actualDivePlan.Cylinders[i]);
            }
        }

        [Fact]
        public void IntergrationTestDenyLoad()
        {
            // Given
            var expectedDivePlan = new DivePlan(TestFixture.FixtureDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            presenter.Setup(p => p.GetConfirmation("Save File?")).Returns(true);
            presenter.Setup(p => p.GetConfirmation("Load File?")).Returns(false);
            fileController = new FileController(presenter.Object, new JsonController(), new());
            fileController.AddDivePlan(divePlan);
            fileController.AddDivePlan(divePlan);

            // When
            fileController.AddDivePlan(expectedDivePlan);
            fileController.AddDivePlan(expectedDivePlan);
            fileController.SaveFile();
            var actualDivePlan = fileController.LoadFile();

            // Then
            presenter.VerifyAll();
            Assert.Null(actualDivePlan);
        }
    }
}