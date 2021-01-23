# This program processes a health data CSV file from the County Health Rankings 
# website and uploads the data contained in the spreadsheet to the ETSU CPH-IVT website.
# 
# ****************************************************************************************
# NOTE:		This code module should be considered INCOMPLETE.  Behavior of this module 
#			may not be consistent with its purpose.	 Proceed with caution.
# ****************************************************************************************
# 
# Created: 10/28/2019
# Creator: Tomas Hill, hilltw@etsu.edu
# 
# Imports Usage
# CHRdataHandlerError:	exceptions related to the CHRdataHandler class
# numpy:				constructing proper series to locate parts of a pandas.DataFrame
# os:					correct directory traversal related to temporary file clean-up
# re:					regular expressions to determine proper file/folder names
# shutil:				removing a temporary folder and all files it contains on clean-up
# 
# Design Improvements
# This module should run on some sort of a timer or as some part of a parent script 
# that runs on a timer.	 Ideally, this processing should occur once a year, when the 
# County Health Rankings (CHR) website releases new data.  Errors returned from the 
# CHRdataHandler module should allow this program to determine if a new file is 
# available for download from the CHR website.	Using these errors (in combination with 
# some sort of logging functionality) would allow the program to adjust itself in the 
# event that data was not ready for download.  The workflow would be similar to the 
# following...
# -- on good download (file was available), reset the program to run in 1 calendar year
# -- on bad download (file was not available), reset the program to run in a reasonable 
#		timeframe, possibly in another week or month to allow time for CHR to post 
#		new data (probably need to determine client preference for this)
# 
# Future Work
# Logging implementation for both good and bad program workflow
# Project integration into an initial migration
# 
from CHRdataHandlerError import *
import math
import numpy
import os
import pandas
import re
import requests
import sys

class CHRdataHandler:
	"""
	Data handler providing functionality related to the transmission of data files from 
	the County Health Rankings website and to the ETSU CPH-IVT website.
	"""
	states = {
		'AK': 'Alaska',
		'AL': 'Alabama',
		'AR': 'Arkansas',
		'AS': 'American Samoa',
		'AZ': 'Arizona',
		'CA': 'California',
		'CO': 'Colorado',
		'CT': 'Connecticut',
		'DC': 'District of Columbia',
		'DE': 'Delaware',
		'FL': 'Florida',
		'GA': 'Georgia',
		'GU': 'Guam',
		'HI': 'Hawaii',
		'IA': 'Iowa',
		'ID': 'Idaho',
		'IL': 'Illinois',
		'IN': 'Indiana',
		'KS': 'Kansas',
		'KY': 'Kentucky',
		'LA': 'Louisiana',
		'MA': 'Massachusetts',
		'MD': 'Maryland',
		'ME': 'Maine',
		'MI': 'Michigan',
		'MN': 'Minnesota',
		'MO': 'Missouri',
		'MP': 'Northern Mariana Islands',
		'MS': 'Mississippi',
		'MT': 'Montana',
		'NC': 'North Carolina',
		'ND': 'North Dakota',
		'NE': 'Nebraska',
		'NH': 'New Hampshire',
		'NJ': 'New Jersey',
		'NM': 'New Mexico',
		'NV': 'Nevada',
		'NY': 'New York',
		'OH': 'Ohio',
		'OK': 'Oklahoma',
		'OR': 'Oregon',
		'PA': 'Pennsylvania',
		'PR': 'Puerto Rico',
		'RI': 'Rhode Island',
		'SC': 'South Carolina',
		'SD': 'South Dakota',
		'TN': 'Tennessee',
		'TX': 'Texas',
		'UM': 'U.S. Minor Outlying Islands',
		'UT': 'Utah',
		'VA': 'Virginia',
		'VI': 'U.S. Virgin Islands',
		'VT': 'Vermont',
		'WA': 'Washington',
		'WI': 'Wisconsin',
		'WV': 'West Virginia',
		'WY': 'Wyoming'
	}
	
	def get_data_file(self, url, filename=None):
		"""
		Retrieves a County Health Rankings file from the given URL in chunks and writes 
		the file to disk in a folder within the current directory of this class file 
		(CHRdataHandler.py).
		
		:param url:			url of CHR health data file to download
		:type url:			string
		
		:param filename:	filename to use for downloaded CHR health data file
		:type filename:		string
		
		:return:			system independent path to folder containing downloaded health 
							data file
		:return type:		string
		
		:FileExistsError:	can occur if the folder being created already exists
		:handling:			pass
		
		:
		"""
		# check parameters
		if filename is None:
			filename = "data.csv"
		else:
			filename += ".csv"
			
		# create system independent path to folder containing latest CHR data file
		folder_path = os.path.join(self.get_path(), "data")
		
		# create folder to hold downloaded data file
		# 
		# TODO: consider what happens here when the folder already exists...this should 
		#		only occur when an attempt is being made to parallelize code using this 
		#		class...maybe a continue is not appropriate? (change folder_path to 
		#		properly continue processing?)
		try:
			os.makedirs(folder_path)
		except FileExistsError:
			pass
			# tb = sys.exc_info()[2]
			# message = "The folder '{}' already exists and cannot be created again".format(folder_path)
			# raise FolderCreationError(message).with_traceback(tb)
		
		print("Retrieving CHR data...\n")
		
		# attempt to open file stream to download data file
		try:
			r = requests.get(url, stream = True)	
		except RequestException:
			tb = sys.exc_info()[2]
			message = "An ambiguous error occurred when handling request using URL '{}'".format(url)
			raise AmbiguousRequestError(message).with_traceback(tb)
		except ConnectionError:
			tb = sys.exc_info()[2]
			message = "A connection error occurred when handling request using URL '{}'".format(url)
			raise RequestConnectionError(message).with_traceback(tb)
		except HTTPError:
			tb = sys.exc_info()[2]
			message = "An HTTP error occurred when handling request using URL '{}'".format(url)
			raise RequestHTTPError(message).with_traceback(tb)
		except URLRequired:
			tb = sys.exc_info()[2]
			message = "Invalid URL '{}' given".format(url)
			raise RequestURLRequiredError(message).with_traceback(tb)
		except TooManyRedirects:
			tb = sys.exc_info()[2]
			message = "Too many redirects when handling request using url '{}'".format(url)
			raise RequestTooManyRedirectsError(message).with_traceback(tb)
			
		# create system independent path to downloaded file
		file_path = os.path.join(folder_path, filename)
		
		# write file to disk
		try:
			with open(file_path, "wb") as file:
				for chunk in r.iter_content(chunk_size = 1024):
					try:
						file.write(chunk)
					except OSError:
						tb = sys.exc_info()[2]
						message = "Write to file '{}' failure".format(file_path)
						raise FileWriteError(message).with_traceback(tb)
		except OSError:
			tb = sys.exc_info()[2]
			message = "Unable to open file using path '{}'".format(file_path)
			raise FileOpenError(message).with_traceback(tb)
		
		# clean the downloaded data file
		self.clean(file_path)

		return file_path


	def create_indicator_files(self, path, output_folder=None, option=None, value=None):
		"""
		Creates multiple csv files from a single County Health Rankings data file.	Each 
		file will contain a single health indicator rather than all indicators existing 
		within a single file.  The individual files will be processed to include 3 columns 
		named "State," "County," and "[Indicator]", where "State" is the state level FIPS code 
		(with leading 0s stripped) corresponding to that row data, "County" is the county 
		level FIPS code (with leading 0s stripped) corresponding to that row data, and 
		"[Indicator]" is the value given at that row.  The names of the created files will 
		match the health indicator that is given at a particular column.  For example, if 
		the file was created by using the "Premature death" health indicator, the file 
		will be created with the same name.	 NaNs will be handled with the given option.  
		Default handling of NaNs is dropping rows for each indicator file that contains 
		them.  If option is 'fill' the NaNs will be filled with the given value, '-1' 
		for default.
		
		:param path:			system independent path to health data file
		:type path:				string
		
		:param output_folder:	name of folder to contain created indicator files
		:type output_folder:	string
		
		:param option:			method of change for NaN values within indicator files
		:type option:			string
		
		:param value:			value to change NaN values to within indicator files
		:type value:			scalar
		
		:param analytics:		if True, create analytics file as well
		:type analytics:		boolean
		
		:return:				None
		"""
		print("Beginning construction of indicator csv files...")
		
		# check parameters
		if output_folder is None:
			output_folder = "indicators"
			
		# create folder to hold downloaded data file
		parent_folder = os.path.join(self.get_path(), "indicators")
		indicators_folder = os.path.join(parent_folder, output_folder)
		
		# create folder to hold downloaded data file
		# 
		# TODO: consider what happens here when the file already exists...this should 
		#		only occur when an attempt is being made to parallelize code using this 
		#		class...maybe a continue is not appropriate? (change folder_path to 
		#		properly continue processing?)
		try:
			os.makedirs(indicators_folder)
		except FileExistsError:
			pass
			# tb = sys.exc_info()[2]
			# message = "The folder '{}' already exists and cannot be created again".format(folder_path)
			# raise FolderCreationError(message).with_traceback(tb)

		# get health indicators from data file
		df, indicators = self.get_indicators(path)
		
		indicators = list(indicators)
		
		# check caller NaN handling option
		if option is 'drop' or option is None:
			self.drop_nans(df, indicators, indicators_folder)
		elif option is 'fill':
			self.fill_nans(df, indicators, indicators_folder, value)
		elif option is 'keep':
			self.keep_nans(df, indicators, indicators_folder)
		else:
			tb = sys.exc_info()[2]
			message = "Invalid option parameter '{}'".format(option)
			raise NanOptionError(message).with_traceback(tb)
		
		print("Construction of individual csv files successfully completed")


	def get_indicators(self, file_location):
		"""
		Constructs indicator files from a County Health Rankings health data file using 
		given file_location.
		
		:param file_location:	system independent path to CHR health data file
		:type file_location:	string
		
		:return:				2-tuple containing a pandas.DataFrame and a list of 
								health indicators
		:return type:			(pandas.DataFrame, [string, string, ..., string])
		"""
		df = pandas.read_csv(file_location, low_memory=False)
		
		if df is None:
			tb = sys.exc_info()[2]
			message = "Unable to read CSV file from '{}'".format(file_location)
			raise ReadCsvError(message).with_traceback(tb)
		
		pattern = re.compile(".*raw value")
		state_info = ["State FIPS Code", "County FIPS Code", "State Abbreviation", "Name", "Release Year"]
		indicators = [x for x in df.columns if pattern.match(x)]
		columns = state_info + indicators
		
		df = pandas.read_csv(file_location, usecols=columns, low_memory=False)
		
		if df is None:
			tb = sys.exc_info()[2]
			message = "Unable to read CSV file from '{}'".format(file_location)
			raise ReadCsvError(message).with_traceback(tb)
		
		return (df, indicators)


	def drop_nans(self, df, indicators, output_folder):
		"""
		Removes all rows from given pandas DataFrame containing NaN values.
		
		:param df:				the DataFrame to modify
		:type df:				pandas.DataFrame
		
		:param indicators:		list of indicators to parse from the DataFrame
		:type indicators:		[string, string, ..., string]
		
		:param output_folder:	folder name to contain created indicator files
		:type output_folder:	string
		
		:return:				None
		"""
		i = 5
		for name in indicators:
			filename = os.path.join(output_folder, "indicator{}".format(i))
			csv_location = os.path.join(self.get_path(), "{}.csv".format(filename))
			
			try:
				self.add_state_name(df).iloc[1:, numpy.r_[0,1,2,3,i,-1]].dropna().to_csv(csv_location, header=["State", "County", "State Abbr", "County Name", name.replace(' raw value', ''), "Full State Name"], index=False)
			except IndexError:
				tb = sys.exc_info()[2]
				message = "Slice indexers out of bounds using 'pandas.DataFrame.iloc' and/or 'numpy.r_'".format(file_location)
				raise PandasIndexError(message).with_traceback(tb)
				
			i += 1


	def fill_nans(self, df, indicators, output_folder, value=-1):
		"""
		Fill all cells from given pandas DataFrame containing NaN values to given value.
		
		:param df:				the DataFrame to modify
		:type df:				pandas.DataFrame
		
		:param indicators:		list of indicators to parse from the DataFrame
		:type indicators:		[string, string, ..., string]
		
		:param output_folder:	folder name to contain created indicator files
		:type output_folder:	string
		
		:param value:			value to change DataFrame NaN values to
		:type value:			scalar
		
		:return:				None
		"""
		i = 5
		for name in indicators:
			filename = os.path.join(output_folder, "indicator{}".format(i))
			csv_location = os.path.join(self.get_path(), "{}.csv".format(filename))
			
			try:
				self.add_state_name(df).iloc[1:, numpy.r_[0,1,2,3,i,-1]].fillna(value).to_csv(csv_location, header=["State", "County", "State Abbr", "County Name", name.replace(' raw value', ''), "Full State Name"], index=False)
			except IndexError:
				tb = sys.exc_info()[2]
				message = "Slice indexers out of bounds using 'pandas.DataFrame.iloc' and/or 'numpy.r_'".format(file_location)
				raise PandasIndexError(message).with_traceback(tb)
				
			i += 1


	def keep_nans(self, df, indicators, output_folder):
		"""
		Removes all rows from given pandas DataFrame containing NaN values.
		
		:param df:				the DataFrame to modify
		:type df:				pandas.DataFrame
		
		:param indicators:		list of indicators to parse from the DataFrame
		:type indicators:		[string, string, ..., string]
		
		:param output_folder:	folder name to contain created indicator files
		:type output_folder:	string
		
		:return:				None
		"""
		i = 5
		for name in indicators:
			filename = os.path.join(output_folder, "indicator{}".format(i))
			csv_location = os.path.join(self.get_path(), "{}.csv".format(filename))
			
			try:
				self.add_state_name(df).iloc[1:, numpy.r_[0,1,2,3,i,-1]].to_csv(csv_location, header=["State", "County", "State Abbr", "County Name", name.replace(' raw value', ''), "Full State Name"], index=False)
			except IndexError:
				tb = sys.exc_info()[2]
				message = "Slice indexers out of bounds using 'pandas.DataFrame.iloc' and/or 'numpy.r_'".format(file_location)
				raise PandasIndexError(message).with_traceback(tb)
				
			i += 1
			
			
	def add_state_name(self, df):
		"""
		"""
		info = []
		info.append("")
		
		for x in range(1, len(df)):
			if not pandas.isnull(df.iloc[x, 2]):
				info.append(self.states[df.iloc[x, 2]])
			else:
				info.append('nan')
			
		df["Full State Name"] = info
		
		return df
	
	def create_analytics_file(self, path, filename=None):
		"""
		Creates a data analytics file from a single year of County Health Rankings data.
		
		:param path:		path to a CHR health data file
		:type path:			string
		
		:param filename:	name of analytics file to create
		:type filename:		string
		
		:return:			None
		"""
		print("Beginning construction of analytics file...")
		
		# check parameters
		if filename is None:
			filename = "analytics.csv"
		else:
			filename += ".csv"
			
		# create system independent path to folder containing latest CHR data file
		folder_path = os.path.join(self.get_path(), "analytics")
		
		# create folder to hold constructed analytics file
		# 
		# TODO: consider what happens here when the folder already exists...this should 
		#		only occur when an attempt is being made to parallelize code using this 
		#		class...maybe a continue is not appropriate? (change folder_path to 
		#		properly continue processing?)
		try:
			os.makedirs(folder_path)
		except FileExistsError:
			pass
			# tb = sys.exc_info()[2]
			# message = "The folder '{}' already exists and cannot be created again".format(folder_path)
			# raise FolderCreationError(message).with_traceback(tb)
			
		# pandas.DataFrame from self.get_indicators method contains desired representation 
		# of analytics file used by the project
		df, indicators = self.get_indicators(path)
		
		# create system independent path to analytics file
		file_path = os.path.join(folder_path, filename)
		
		df.to_csv(file_path)
		
		print("Construction of analytics file successfully completed")


	def combine_analytics_files(self, input_folder, output_file=None):
		"""
		Combines a group of analytics files into one.
		
		:param input_folder:	name of folder containing the analytics files to combine
		:type input_folder:		string
		
		:param output_file:		name of combined analytics file to produce
		:type output_file:		string
		
		:return:				None
		"""
		print("Combining analytics files...")
		
		# check parameters
		if output_file is None:
			output_file = os.path.join(self.get_path(), "analytics.csv")
		else:
			output_file = os.path.join(self.get_path(), "{}.csv".format(output_file))
			
		# construct system independent path to input_folder
		input_path = os.path.join(self.get_path(), input_folder)
		
		files = os.listdir(input_path)
		
		# get column names from first CSV in input_folder
		df = pandas.read_csv(os.path.join(input_path, files[0]))
		columns = df.columns[1:]
		
		# construct empty DataFrame to house combined analytics files
		df = pandas.DataFrame()
		
		# combine each analytics file
		for file in os.listdir(input_path):
			path = os.path.join(input_path, file)
			next_file = pandas.read_csv(path, index_col=False)
			df = df.append(next_file.iloc[1:,:], ignore_index=True, sort=False)
		
		# drop unnecessary index column from DataFrame
		df.drop(["Unnamed: 0"], axis=1, inplace=True)
		
		df.to_csv(os.path.join(self.get_path(), "CHR_analytics.csv"))
		
		print("Combining data files successfully completed")


	def clean(self, file):
		"""
		Remove unusable data from given file.  The application does not consider full 
		United States data or full state data because it is calculated and can be 
		generated on its own.
		
		:param file:	file to clean
		
		:return:		None
		"""
		df = pandas.read_csv(file, low_memory=False)
		
		if df is None:
			tb = sys.exc_info()[2]
			message = "Unable to read CSV file from '{}'".format(file_location)
			raise ReadCsvError(message).with_traceback(tb)
		
		for row in df.iterrows():
			if row[1][0] == '00' or row[1][1] == '000' or row[1][6] == '0':
				df.drop(row[0], inplace=True)
				
		df.to_csv(file)


	def get_path(self):
		"""
		Retrieve system independent path to this file (CHRdataHander.py).
		
		:param:		None
		
		:return:	None
		"""
		return os.path.dirname(os.path.realpath(sys.argv[0]))