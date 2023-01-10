using BubblesDivePlanner.Presenters;

namespace BubblesDivePlanner.Controllers
{
    public class DecompressionController : IDecompressionController
    {
        private IDivePresenter divePresenter;
        private IDiveController diveController;

        public DecompressionController(IDivePresenter divePresenter, IDiveController diveController)
        {
            this.divePresenter = divePresenter;
            this.diveController = diveController;
        }

        public void RunDecompression()
        {
            divePresenter.ConfirmDecompression();
        }
    }
}