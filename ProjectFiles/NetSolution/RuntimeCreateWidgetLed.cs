#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
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
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.OPCUAClient;
using FTOptix.RAEtherNetIP;
#endregion

public class RuntimeCreateWidgetLed : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var myPage = Owner;
        var myLabel = InformationModel.Make<Label>("Test");
        myLabel.HorizontalAlignment = HorizontalAlignment.Center;
        myLabel.Text = "Hello Scripting Screen";
        myPage.Add(myLabel);
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        Log.Info("App is closing");
    }

    [ExportMethod]
    public void CreateType(int numIstanze)
    {
        var myTarget = Owner.Get("VerticalLayout");
        for (int i = 0; i < numIstanze; i++)
        {
            var myWidgetLed = InformationModel.Make<ToggleLed>(NomeIstanza(i));

            myTarget.Add(myWidgetLed);

        }
    }

    public string NomeIstanza(int numero)
    {
        return "WidgetLed" + numero;
    }
}
