using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;

namespace BubblesDivePlanner.Presenters
{
    public class DiveSetup : IDiveSetup
    {
        private readonly IPresenter presenter;

        public DiveSetup(IPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void WelcomeMessage()
        {
            presenter.Print("Bubbles Dive Planner Console");
        }

        public DiveStep CreateDiveStep()
        {
            return new DiveStep(presenter.GetByte("Enter Depth:"), presenter.GetByte("Enter Time:"));
        }

        public Cylinder CreateCylinder()
        {
            var gasMixture = new GasMixture(presenter.GetByte("Enter Oxygen:"), presenter.GetByte("Enter Helium:"));
            return new Cylinder(presenter.GetUnsignedInteger16("Enter Cylinder Volume:"), presenter.GetUnsignedInteger16("Enter Cylinder Pressure:"), gasMixture, presenter.GetByte("Enter Surface Air Consumption Rate:"));
        }
    }
}