///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         SavedChartCounties.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            Capstone
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Services.Repos
{
    using CPH.Services.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SavedChartCounties" />.
    /// </summary>
    public class SavedChartCounties : ISavedChartCounties
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedChartsCounties">The savedChartsCounties<see cref="Models.SavedChartCounties"/>.</param>
        /// <returns>The <see cref="Models.SavedChartCounties"/>.</returns>
        public Models.SavedChartCounties Create(Models.SavedChartCounties savedChartsCounties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedChartsCounties">The savedChartsCounties<see cref="Models.SavedChartCounties"/>.</param>
        public void Delete(Models.SavedChartCounties savedChartsCounties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.SavedChartCounties"/>.</returns>
        public Models.SavedChartCounties Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.SavedChartCounties}"/>.</returns>
        public ICollection<Models.SavedChartCounties> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedChartsCounties">The savedChartsCounties<see cref="Models.SavedChartCounties"/>.</param>
        /// <returns>The <see cref="Models.SavedChartCounties"/>.</returns>
        public Models.SavedChartCounties Update(Models.SavedChartCounties savedChartsCounties)
        {
            throw new NotImplementedException();
        }
    }
}
