///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         SavedChartYear.cs
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
    /// Defines the <see cref="SavedChartYear" />.
    /// </summary>
    public class SavedChartYear : ISavedChartYear
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedChartYear">The savedChartYear<see cref="Models.SavedChartYear"/>.</param>
        /// <returns>The <see cref="Models.SavedChartYear"/>.</returns>
        public Models.SavedChartYear Create(Models.SavedChartYear savedChartYear)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedChartYear">The savedChartYear<see cref="Models.SavedChartYear"/>.</param>
        public void Delete(Models.SavedChartYear savedChartYear)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.SavedChartYear"/>.</returns>
        public Models.SavedChartYear Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.SavedChartYear}"/>.</returns>
        public ICollection<Models.SavedChartYear> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedChartYear">The savedChartYear<see cref="Models.SavedChartYear"/>.</param>
        /// <returns>The <see cref="Models.SavedChartYear"/>.</returns>
        public Models.SavedChartYear Update(Models.SavedChartYear savedChartYear)
        {
            throw new NotImplementedException();
        }
    }
}
