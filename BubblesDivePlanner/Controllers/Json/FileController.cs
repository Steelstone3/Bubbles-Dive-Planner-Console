using System;
using System.IO;
using BubblesDivePlanner.Models.DivePlans;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlanner.Controllers.Json
{
    public class FileController : IFileController
    {
        private const string FILE_NAME = "dive_plan.json";
        private readonly IPresenter presenter;

        public FileController(IPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void SaveFile(IDivePlan divePlan)
        {
            if (presenter.GetConfirmation("Save File?"))
            {
                try
                {
                    using StreamWriter writer = new(FILE_NAME);
                    writer.Write(divePlan.Serialise());
                }
                catch (Exception)
                {
                    presenter.Print($"{FILE_NAME} could not be found or written to.");
                }
            }
        }

        public IDivePlan LoadFile()
        {
            if (presenter.GetConfirmation("Load File?"))
            {
                try
                {
                    var fileContent = File.ReadAllText(FILE_NAME);
                    var divePlan = new DivePlan(null, null, null, null);
                    divePlan.Deserialise(fileContent);

                    return divePlan;
                }
                catch (Exception)
                {
                    presenter.Print($"{FILE_NAME} could not be found or read from.");
                }
            }

            return null;
        }
    }
}