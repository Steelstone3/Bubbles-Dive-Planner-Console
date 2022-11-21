using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DivePlans;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlanner.Controllers
{
    public class DiveController : IDiveController
    {
        private readonly IDiveStepPresenter diveStepPresenter;
        private readonly IDiveSetupPresenter diveSetupPresenter;
        private readonly IDiveStagesController diveStagesController;
        private IDiveModel diveModel;
        private List<ICylinder> cylinders;

        public DiveController(IDiveStepPresenter diveStepPresenter, IDiveSetupPresenter diveSetupPresenter, IDiveStagesController diveController)
        {
            this.diveStepPresenter = diveStepPresenter;
            this.diveSetupPresenter = diveSetupPresenter;
            this.diveStagesController = diveController;
        }

        public void SetupDivePlan(IDivePlan divePlan)
        {
            diveSetupPresenter.WelcomeMessage();

            if (divePlan == null)
            {
                diveModel = diveSetupPresenter.SelectDiveModel();
                cylinders = diveSetupPresenter.CreateCylinders();
            }
            else
            {
                if (divePlan.DiveModel == null)
                {
                    diveModel = diveSetupPresenter.SelectDiveModel();
                }
                else
                {
                    diveModel = divePlan.DiveModel;
                }

                if (divePlan.Cylinders == null)
                {
                    cylinders = diveSetupPresenter.CreateCylinders();
                }
                else
                {
                    cylinders = divePlan.Cylinders;
                }
            }
        }

        public IDivePlan SetupDiveStep()
        {
            return new DivePlan(diveModel, cylinders, diveStepPresenter.CreateDiveStep(), diveStepPresenter.SelectCylinder(cylinders));
        }

        public IDivePlan RunDiveProfile(IDivePlan divePlan)
        {
            return diveStagesController.Run(divePlan);
        }

        public void PrintDiveResults(IDivePlan divePlan)
        {
            diveSetupPresenter.PrintDiveResults(divePlan);
        }
    }
}