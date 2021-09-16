from django.apps import apps
from django.db import migrations, models
import os
import subprocess
import sys
import pandas

script_directory = os.path.join(os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__)))), 'data_scripts')
script_file_path = os.path.join(script_directory, 'refresh_website_data.py') 

states = set()
counties = []
indicators = []

def initialize_database():
	# subprocess.call(["python",script_file_path])
	indicators_directory = os.path.join(script_directory, 'indicators')
	indicator_containers = [x for x in os.walk(indicators_directory)]
	indicator_year_directories = [x[0] for x in indicator_containers[1:]]
	years = [x[-15:-11] for x in indicator_year_directories]
		
	current_directory_file_index = 0
	current_fips = 0

	for directory in indicator_year_directories:
		for file in os.listdir(directory):
			file_path = os.path.join(indicator_year_directories[current_directory_file_index], file)
			df = pandas.read_csv(file_path)
			
			current_fips = df.iloc[0,0]
			
			
			HealthIndicator = apps.get_model('app', 'HealthIndicator')
			State = apps.get_model('app', 'State')
			County = apps.get_model('app', 'County')
			
			# fill lists
			for index, row in df.iterrows():
			
				if current_fips != df.iloc[index, 0]:
				
					indicator_objects = []
					for value in indicators:
						indicator_objects.append(HealthIndicator(name=df.columns.values[4], year=years[current_directory_file_index], value=value))
					
					county_objects = []
					for county in counties:
						county_objects.append(County(fips=county[0], name=county[1], indicators=indicator_objects))
						
					state = State(abbreviation=df.iloc[index, 2], name=df.iloc[index, -1], fips=current_fips, counties=county_objects)
					state.save()

					# reset lists
					states.clear()
					counties.clear()
					indicators.clear()

					current_fips = df.iloc[index, 0]
					states.add(row['State'])
				else:
					states.add(row['State'])
					counties.append((row['County'], row['County Name']))
					indicators.append(row[df.columns.values[4]])
				
			# reset lists
			states.clear()
			counties.clear()
			indicators.clear()
			
		current_directory_file_index += 1
		
		
class Migration(migrations.Migration):

    dependencies = [
        ('app', '0001_initial'),
    ]

    operations = [
        migrations.RunPython(initialize_database),
    ]