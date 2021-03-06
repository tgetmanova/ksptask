﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.IO;

namespace UserRepositoryServiceTests.Utils
{
    /// <summary>
    /// Test Util methods.
    /// </summary>
    internal class TestUtils
    {
        /// <summary>
        /// Aggregates the assertions.
        /// </summary>
        /// <param name="actions">The actions.</param>
        /// <exception cref="Xunit.Sdk.XunitException">Exception aggregating all error messages. </exception>
        internal static void AggregateAssertions(params Action[] actions)
        {
            var exceptionMessages = new List<string>();

            foreach (var action in actions)
            {
                try
                {
                    action.Invoke();
                }
                catch (Xunit.Sdk.XunitException xUnitException)
                {
                    exceptionMessages.Add(xUnitException.Message);
                }
            }

            if (exceptionMessages.Any())
            {
                throw new Xunit.Sdk.XunitException(string.Join($";{Environment.NewLine}", exceptionMessages));
            }
        }

        /// <summary>
        /// Determines whether [is date time in the expected range] [the specified date to check].
        /// </summary>
        /// <param name="dateToCheck">The date to check.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns> <c>true</c> if [is date time in the expected range] [the specified date to check]; otherwise, <c>false</c>. /// </returns>
        public static bool IsDateTimeInTheExpectedRange(DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }

        /// <summary>
        /// Gets the log entries.
        /// </summary>
        /// <returns>File log read lines. </returns>
        internal static IEnumerable<string> GetLogEntries()
        {
            var pathToFile = ConfigurationManager.AppSettings["PathToLogFile"];
           
            using (var fileStream = File.Open(pathToFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
