///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         ISavedCharts.cs
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
    /// Defines the <see cref="ISavedCharts" />.
    /// </summary>
    public interface ISavedCharts
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedCharts">The savedCharts<see cref="SavedCharts"/>.</param>
        /// <returns>The <see cref="SavedCharts"/>.</returns>
        SavedCharts Create(SavedCharts savedCharts);

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="SavedCharts"/>.</returns>
        SavedCharts Read(int id);

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{SavedCharts}"/>.</returns>
        ICollection<SavedCharts> ReadAll();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedCharts">The savedCharts<see cref="SavedCharts"/>.</param>
        /// <returns>The <see cref="SavedCharts"/>.</returns>
        SavedCharts Update(SavedCharts savedCharts);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedCharts">The savedCharts<see cref="SavedCharts"/>.</param>
        void Delete(SavedCharts savedCharts);
    }
}
