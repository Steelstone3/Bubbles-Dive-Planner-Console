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
        private readonly IFilePresenter filePresenter;
        private readonly IJsonController jsonController;
        private readonly List<IDivePlan> divePlans;

        public FileController(IFilePresenter presenter, IJsonController jsonController, List<IDivePlan> divePlans)
        {
            this.filePresenter = presenter;
            this.jsonController = jsonController;
            this.divePlans = divePlans;
        }

        public void AddDivePlan(IDivePlan divePlan)
        {
            divePlans.Add(divePlan);
        }

        public void SaveFile()
        {
            if (filePresenter.DisplaySaveFile())
            {
                try
                {
                    using StreamWriter writer = new(FILE_NAME);
                    var serialisedDivePlans = jsonController.Serialise(divePlans);
                    writer.Write(serialisedDivePlans);
                }
                catch (Exception)
                {
                    filePresenter.DisplaySaveErrorMessage(FILE_NAME);
                }
            }
        }

        public IDivePlan LoadFile()
        {
            if (filePresenter.DisplayLoadFile())
            {
                try
                {
                    var fileContent = File.ReadAllText(FILE_NAME);
                    return jsonController.Deserialise(fileContent);
                }
                catch (Exception)
                {
                    filePresenter.DisplayLoadErrorMessage(FILE_NAME);
                }
            }

            return null;
        }
    }
}