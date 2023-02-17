#region Using directives
using System;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NetLogic;
using FTOptix.OPCUAServer;
using FTOptix.UI;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.DataLogger;
using FTOptix.Report;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.EventLogger;
using FTOptix.OPCUAClient;
using FTOptix.RAEtherNetIP;
using FTOptix.EthernetIP;
#endregion

public class CreateUserPanelLogic : BaseNetLogic
{
    [ExportMethod]
    public void CreateUser(string username, string password, string locale, out NodeId result)
    {
		result = NodeId.Empty;
		if (string.IsNullOrEmpty(username))
		{
			Log.Error("CreateUserPanelLogic", "Cannot create user with empty username");
			return;
		}

		result = GenerateUser(username, password, locale);
    }

    private NodeId GenerateUser(string username, string password, string locale)
    {
        var user = InformationModel.MakeObject<FTOptix.Core.User>(username);
        user.LocaleId = locale;

		var users = GetUsers();
		if (users == null)
		{
			Log.Error("CreateUserPanelLogic", "Unable to get users");
			return NodeId.Empty;
		}

		users.Add(user);
		var result = Session.ChangePassword(username, password, string.Empty);
		if (result.ResultCode == FTOptix.Core.ChangePasswordResultCode.PasswordTooShort)
		{
			Log.Error("CreateUserPanelLogic", "Password too short");
			var errorMessageVariable = LogicObject.GetVariable("ErrorMessage");
			if (errorMessageVariable != null)
				errorMessageVariable.Value = "Password too short";

			users.Remove(user);
			return NodeId.Empty;
		}

		return user.NodeId;
	}

    private IUANode GetUsers()
	{
		var pathResolverResult = LogicObject.Context.ResolvePath(LogicObject, "{Users}");
		if (pathResolverResult == null)
			return null;
		if (pathResolverResult.ResolvedNode == null)
			return null;

		return pathResolverResult.ResolvedNode;
	}
}
