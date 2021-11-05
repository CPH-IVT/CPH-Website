///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         ISavedChartDatapoints.cs
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
    /// Defines the <see cref="ISavedChartDatapoints" />.
    /// </summary>
    public interface ISavedChartDatapoints
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedChartDatapoints">The savedChartDatapoints<see cref="SavedChartDatapoints"/>.</param>
        /// <returns>The <see cref="SavedChartDatapoints"/>.</returns>
        SavedChartDatapoints Create(SavedChartDatapoints savedChartDatapoints);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="SavedChartDatapoints"/>.</returns>
        SavedChartDatapoints Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{SavedChartDatapoints}"/>.</returns>
        ICollection<SavedChartDatapoints> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedChartDatapoints">The savedChartDatapoints<see cref="SavedChartDatapoints"/>.</param>
        /// <returns>The <see cref="SavedChartYear"/>.</returns>
        SavedChartYear Update(SavedChartDatapoints savedChartDatapoints);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedChartDatapoints">The savedChartDatapoints<see cref="SavedChartDatapoints"/>.</param>
        void Delete(SavedChartDatapoints savedChartDatapoints);
    }
}
