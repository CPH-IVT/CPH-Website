# This program processes a health data CSV file from the County Health Rankings 
# website and uploads the data contained in the spreadsheet to the ETSU CPH-IVT website.
# 
# ****************************************************************************************
# NOTE: 	This code module should be considered INCOMPLETE.  Behavior of this module 
#			may not be consistent with its purpose.  Proceed with caution.
# ****************************************************************************************
# 
# Created: 11/5/2019
# Creator: Tomas Hill, hilltw@etsu.edu
# 
# Imports Usage
# CHRdataHandler:		all processing related to a County Health Rankings CSV file
# CHRdataHandlerError:	exceptions related to the CHRdataHandler class
# os:					correct directory traversal related to temporary file clean-up
# re:					regular expressions to determine proper file/folder names
# shutil:				removing a temporary folder and all files it contains on clean-up
# 
# Design Improvements
# This module should run on some sort of a timer or as some part of a parent script 
# that runs on a timer.  Ideally, this processing should occur once a year, when the 
# County Health Rankings (CHR) website releases new data.  Errors returned from the 
# CHRdataHandler module should allow this program to determine if a new file is 
# available for download from the CHR website.  Using these errors (in combination with 
# some sort of logging functionality) would allow the program to adjust itself in the 
# event that data was not ready for download.  The workflow would be similar to the 
# following...
# -- on good download (file was available), reset the program to run in 1 calendar year
# -- on bad download (file was not available), reset the program to run in a reasonable 
# 		timeframe, possibly in another week or month to allow time for CHR to post 
#		new data (probably need to determine client preference for this)
# 
# Future Work
# Logging implementation for both good and bad program workflow
# Project integration into an initial migration
# Decoupling of data pull and indicator creation
# 
from CHRdataHandler import CHRdataHandler
from CHRdataHandlerError import *
import os
import re
import shutil

def main():
	"""
	Main entry point.
	
	:param:  None
	:return: None
	"""
	data_files = [
		"https://www.countyhealthrankings.org/sites/default/files/analytic_data2019.csv",
		"https://www.countyhealthrankings.org/sites/default/files/analytic_data2018_0.csv",
		"https://www.countyhealthrankings.org/sites/default/files/analytic_data2017.csv",
		"https://www.countyhealthrankings.org/sites/default/files/analytic_data2016.csv",
		"https://www.countyhealthrankings.org/sites/default/files/analytic_data2015.csv",
		"https://www.countyhealthrankings.org/sites/default/files/analytic_data2014.csv",
		"https://www.countyhealthrankings.org/sites/default/files/analytic_data2013.csv",
		"https://www.countyhealthrankings.org/sites/default/files/analytic_data2012.csv",
		"https://www.countyhealthrankings.org/sites/default/files/analytic_data2011.csv",
		"https://www.countyhealthrankings.org/sites/default/files/analytic_data2010.csv"
	]
	
	handler = CHRdataHandler()
		
	# retrieve all data files from CHR
	data_file_paths = []
	
	# 
	# TODO: errors should be logged rather than printed
	for file in data_files:
		year = re.search(r"\d{4}", file).group(0)
		filename = "{}_data".format(year)
		
		try:
			data_file_paths.append(handler.get_data_file(file, filename))
		except FatalCHRdataHandlerError as fatal:
			print(fatal.expression)
		
	# construct indicator files and analytics files from downloaded health data files
	for path in data_file_paths:
		year = re.search(r"\d{4}", path).group(0)
		indicator_folder = "{}_indicators".format(year)
		
		try:
			handler.create_indicator_files(path, indicator_folder)
		except FatalCHRdataHandlerError as fatal:
			print(fatal.expression)
		
		analytics_filename = "{}_analytics".format(year)
		
		try:
			handler.create_analytics_file(path, analytics_filename)
		except FatalCHRdataHandlerError as fatal:
			print(fatal.expression)
	
	# construct combined analytics file
	handler.combine_analytics_files(os.path.join(handler.get_path(), "analytics"))
	
	# TODO: upload indicator files
	# TODO: clean-up of temporary files
	
	

if __name__ == "__main__":
	main()