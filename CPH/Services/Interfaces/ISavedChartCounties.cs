///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         ISavedChartCounties.cs
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
    /// Defines the <see cref="ISavedChartCounties" />.
    /// </summary>
    public interface ISavedChartCounties
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedChartsCounties">The savedChartsCounties<see cref="SavedChartCounties"/>.</param>
        /// <returns>The <see cref="SavedChartCounties"/>.</returns>
        SavedChartCounties Create(SavedChartCounties savedChartsCounties);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="SavedChartCounties"/>.</returns>
        SavedChartCounties Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{SavedChartCounties}"/>.</returns>
        ICollection<SavedChartCounties> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedChartsCounties">The savedChartsCounties<see cref="SavedChartCounties"/>.</param>
        /// <returns>The <see cref="SavedChartCounties"/>.</returns>
        SavedChartCounties Update(SavedChartCounties savedChartsCounties);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedChartsCounties">The savedChartsCounties<see cref="SavedChartCounties"/>.</param>
        void Delete(SavedChartCounties savedChartsCounties);
    }
}
