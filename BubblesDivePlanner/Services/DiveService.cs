using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlannerTests.Services
{
    public class DiveService : IDiveService
    {
        private readonly IDiveSetup diveSetup;
        private readonly IDivePlan divePlan;
        private IDiveModel diveModel;

        public DiveService(IDiveSetup diveSetup, IDivePlan divePlan)
        {
            this.diveSetup = diveSetup;
            this.divePlan = divePlan;
        }

        public void SetupDivePlan()
        {
            divePlan.WelcomeMessage();
            diveModel = divePlan.SelectDiveModel();
        }
    }
}