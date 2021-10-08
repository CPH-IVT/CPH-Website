///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         SavedChartDatapoints.cs
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
    /// Defines the <see cref="SavedChartDatapoints" />.
    /// </summary>
    public class SavedChartDatapoints : ISavedChartDatapoints
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="savedChartDatapoints">The savedChartDatapoints<see cref="Models.SavedChartDatapoints"/>.</param>
        /// <returns>The <see cref="Models.SavedChartDatapoints"/>.</returns>
        public Models.SavedChartDatapoints Create(Models.SavedChartDatapoints savedChartDatapoints)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="savedChartDatapoints">The savedChartDatapoints<see cref="Models.SavedChartDatapoints"/>.</param>
        public void Delete(Models.SavedChartDatapoints savedChartDatapoints)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Models.SavedChartDatapoints"/>.</returns>
        public Models.SavedChartDatapoints Read(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ReadAll.
        /// </summary>
        /// <returns>The <see cref="ICollection{Models.SavedChartDatapoints}"/>.</returns>
        public ICollection<Models.SavedChartDatapoints> ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="savedChartDatapoints">The savedChartDatapoints<see cref="Models.SavedChartDatapoints"/>.</param>
        /// <returns>The <see cref="Models.SavedChartYear"/>.</returns>
        public Models.SavedChartYear Update(Models.SavedChartDatapoints savedChartDatapoints)
        {
            throw new NotImplementedException();
        }
    }
}
