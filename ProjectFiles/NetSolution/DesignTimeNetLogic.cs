#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.CoreBase;
using FTOptix.Alarm;
using FTOptix.DataLogger;
using FTOptix.EventLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.Report;
using FTOptix.EthernetIP;
using FTOptix.Retentivity;
using FTOptix.Recipe;
using FTOptix.CommunicationDriver;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.OPCUAClient;
using FTOptix.RAEtherNetIP;
#endregion

public class DesignTimeNetLogic : BaseNetLogic
{
    [ExportMethod]
    public void CreateMotors()
    {
        // Create MotorType Instances 
        var myPage = Project.Current.Get("UI/Screens/Calendaring/Scripting");
        var myMotorType = Project.Current.Get("UI/Templates/Motor");

        for (int i = 0; i < 5; i++)
        {
            var myMotor = InformationModel.MakeObject("Motor" + i, myMotorType.NodeId);
            myPage.Add(myMotor);
        }
    }

    [ExportMethod]
    public void CreateCustomWidget()
    {
        // Create Custom WidgetType  
        var myPage = Project.Current.Get("UI/Screens/Calendaring/Scripting");
        var myWidgetType = Project.Current.Get("UI/Templates/CustomWidget");

        for (int i = 0; i < 2; i++)
        {
            var myWidget = InformationModel.MakeObject("CustomWidget" + i, myWidgetType.NodeId);
            myPage.Add(myWidget);
        }
    }
}
