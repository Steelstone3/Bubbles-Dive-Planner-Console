using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Spectre.Console;

namespace BubblesDivePlanner.Presenters
{
    public interface IPresenter
    {
        void Print(string message);
        void WriteResult(Table table);
        void WriteResult(BarChart table);
        string GetString(string message);
        byte GetByte(string message, byte lowerBound, byte upperBound);
        ushort GetUshort(string message, ushort lowerBound, ushort upperBound);
        bool GetConfirmationDefaultYes(string message);
        bool GetConfirmationDefaultNo(string message);
        IDiveModel DiveModelSelection();
        ICylinder CylinderSelection(List<ICylinder> cylinders);
        string HelpMenuSelection();
        Table AssignDiveProfileTable(string name, IDiveProfile diveProfile);
        BarChart AssignDiveProfileChart(string name, IDiveProfile diveProfile);
        Table AssignDiveStepTable(IDiveStep diveStep, string depthCeiling);
        Table AssignCylindersTable(List<ICylinder> cylinders);
    }
}