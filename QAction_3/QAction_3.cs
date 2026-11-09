using System;
using System.Collections.Generic;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: Fill Traps Table.
/// </summary>
public static class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocolExt protocol)
	{
		try
		{
			int rowsToAddCount = Convert.ToInt32(protocol.GetParameter(Parameter.amountofrowstoadd_4));

			var random = new Random();
			DateTime startDate = DateTime.Now.Subtract(new TimeSpan(14, 0, 0, 0));
			TimeSpan timeSpan = DateTime.Now - startDate;

			var trapsRows = new List<QActionTableRow>();
			for (int i = 0; i < rowsToAddCount; i++)
			{
				TimeSpan newSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
				DateTime newDate = startDate + newSpan;

				var row = new TrapsQActionRow()
				{
					Trapsindex_201 = Guid.NewGuid().ToString(),
					Trapsdatetime_202 = newDate.ToOADate(),
				};

				trapsRows.Add(row);
			}

			protocol.traps.FillArrayNoDelete(trapsRows);
		}
		catch (Exception ex)
		{
			protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
		}
	}
}