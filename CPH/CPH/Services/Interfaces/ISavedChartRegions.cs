///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         ISavedChartRegions.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            Capstone
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Services.Interfaces
{
    using CPH.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ISavedChartRegions" />.
    /// </summary>
    public interface ISavedChartRegions
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedChartRegions">The savedChartRegions<see cref="SavedChartRegions"/>.</param>
        /// <returns>The <see cref="SavedChartRegions"/>.</returns>
        SavedChartRegions Create(SavedChartRegions savedChartRegions);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="SavedChartRegions"/>.</returns>
        SavedChartRegions Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{SavedChartRegions}"/>.</returns>
        ICollection<SavedChartRegions> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedChartRegions">The savedChartRegions<see cref="SavedChartRegions"/>.</param>
        /// <returns>The <see cref="SavedChartRegions"/>.</returns>
        SavedChartRegions Update(SavedChartRegions savedChartRegions);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedChartRegions">The savedChartRegions<see cref="SavedChartRegions"/>.</param>
        void Delete(SavedChartRegions savedChartRegions);
    }
}
