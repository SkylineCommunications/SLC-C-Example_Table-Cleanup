using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.TableCleanup;
using Skyline.DataMiner.Utils.TableCleanup.Filters;

/// <summary>
/// DataMiner QAction Class: Cleanup Tables.
/// </summary>
public static class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocol protocol)
	{
		try
		{
			TableCleaner tableCleaner = new TableCleaner(
				protocol,
				Parameter.Traps.tablePid,
				Parameter.Traps.Idx.trapsindex_201,
				Parameter.Traps.Idx.trapsdatetime_202)
			.WithCondition(new TableMaxRowCondition(
				Parameter.cleanupmethod_100,
				Parameter.maximumrowcount_104,
				Parameter.maximumrowage_102))
			.Cleanup();
		}
		catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}