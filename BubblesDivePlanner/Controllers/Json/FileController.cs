using System;
using System.IO;
using BubblesDivePlanner.Models;
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
                    using (StreamWriter writer = new StreamWriter(FILE_NAME))
                    {
                        writer.Write(divePlan.Serialise());
                    }
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


/*public void SerialiseApplication(List<IEntityModel> entityModels, string fileName)
   {
       //string fileName = $"DivePlan.json";
       try
       {
           using (StreamWriter writer = new StreamWriter(fileName))
           {
               var jsonFile = string.Empty;
               //TODO AH may need to change this to have each entity model hosted in a container
               //TODO AH } closing needs to be removed until the true last entity model. Also consider first comment
               foreach (var entityModel in entityModels)
               {
                   jsonFile += JsonConvert.SerializeObject(entityModel, Formatting.Indented);
               }
               writer.Write(jsonFile);
           }
       }
       catch (UnauthorizedAccessException uaex)
       {
           Console.Write(uaex.Message);
       }
       catch (IOException ioex)
       {
           Console.Write(ioex.Message);
       }
       catch (Exception ex)
       {
           Console.Write(ex.Message);
       }
   }
   public IEnumerable<IEntityModel> DeserialiseApplication(string fileResult)
   {
       var fileContents = FilePathToFileContents(fileResult);
       //TODO AH Invalid cast
       yield return (DivePlanEntityModel)JsonConvert.DeserializeObject(fileContents);
       yield return (DiveInfoEntityModel)JsonConvert.DeserializeObject(fileContents);
       yield return (DiveResultsEntityModel)JsonConvert.DeserializeObject(fileContents);
       yield return (DiveHeaderEntityModel)JsonConvert.DeserializeObject(fileContents);         
   }
   //TODO AH Ensure the whole thing can load, catch exceptions
   private string FilePathToFileContents(string fileResult) => JObject.Parse(File.ReadAllText(fileResult)).ToString();*/
