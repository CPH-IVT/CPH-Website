///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         ISavedChartKeywords.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            Capstone
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Services
{
    using CPH.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ISavedChartKeywords" />.
    /// </summary>
    public interface ISavedChartKeywords
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedChartKeywords">The savedChartKeywords<see cref="SavedChartKeywords"/>.</param>
        /// <returns>The <see cref="SavedChartKeywords"/>.</returns>
        SavedChartKeywords Create(SavedChartKeywords savedChartKeywords);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="SavedChartKeywords"/>.</returns>
        SavedChartKeywords Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{SavedChartKeywords}"/>.</returns>
        ICollection<SavedChartKeywords> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedChartKeywords">The savedChartKeywords<see cref="SavedChartKeywords"/>.</param>
        /// <returns>The <see cref="SavedChartKeywords"/>.</returns>
        SavedChartKeywords Update(SavedChartKeywords savedChartKeywords);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedChartKeywords">The savedChartKeywords<see cref="SavedChartKeywords"/>.</param>
        void Delete(SavedChartKeywords savedChartKeywords);
    }
}
