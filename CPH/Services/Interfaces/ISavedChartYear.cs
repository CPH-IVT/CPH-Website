///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         ISavedChartYear.cs
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
    /// Defines the <see cref="ISavedChartYear" />.
    /// </summary>
    public interface ISavedChartYear
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedChartYear">The savedChartYear<see cref="SavedChartYear"/>.</param>
        /// <returns>The <see cref="SavedChartYear"/>.</returns>
        SavedChartYear Create(SavedChartYear savedChartYear);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="SavedChartYear"/>.</returns>
        SavedChartYear Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{SavedChartYear}"/>.</returns>
        ICollection<SavedChartYear> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedChartYear">The savedChartYear<see cref="SavedChartYear"/>.</param>
        /// <returns>The <see cref="SavedChartYear"/>.</returns>
        SavedChartYear Update(SavedChartYear savedChartYear);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedChartYear">The savedChartYear<see cref="SavedChartYear"/>.</param>
        void Delete(SavedChartYear savedChartYear);
    }
}
