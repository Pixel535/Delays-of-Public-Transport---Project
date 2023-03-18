# Delays of Public Transport - Project
The project was done with a group colleague as part of the .NET course for the Military University of Technology

# Description
The application runs on two separate processes communicating via files. 
The first process is the passenger process - it simulates, for example, a user waiting at a stop,
who takes out his or her phone and activates the application to check how long it will take 
to reach his or her The second process is that of the public transport system, which simulates a system that receives the relevant information from 
the passenger process in order to calculate, depending on the the random delay encountered, and communicate back to the user in what time his or her 
chosen means of transport. Each means of transport moves in a loop - from one border stop to another, and then returns along the same route with the same stops. 
Along the way, each means of transport may encounter random events causing the vehicle to be delayed.

The whole programme is based on data we have invented, which will be presented with more detail in the report.

# Getting started
A specific requirement for the configuration of the environment to run and dynamic evaluation of the solution is that the location of the .txt files 
should remain in the the default project folder.

# Used Technologies
* .NET 6.0
* SDK 6
