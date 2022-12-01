using System;
using System.Collections.Generic;
using System.IO;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlanner.Controllers.Json
{
    public class FileController : IFileController
    {
        private const string FILE_NAME = "dive_plan.json";
        private readonly IPresenter presenter;
        private readonly IJsonController jsonController;
        private readonly List<IDivePlan> divePlans;

        public FileController(IPresenter presenter, IJsonController jsonController, List<IDivePlan> divePlans)
        {
            this.presenter = presenter;
            this.jsonController = jsonController;
            this.divePlans = divePlans;
        }

        public void AddDivePlan(IDivePlan divePlan)
        {
            divePlans.Add(divePlan);
        }

        public void SaveFile()
        {
            if (presenter.GetConfirmation("Save File?"))
            {
                try
                {
                    using StreamWriter writer = new(FILE_NAME);
                    var serialisedDivePlans = jsonController.Serialise(divePlans);
                    writer.Write(serialisedDivePlans);
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
                    return jsonController.Deserialise(fileContent);
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