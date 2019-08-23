library(dplyr)
library(ggplot2)

data <- read.csv("sample_data.csv", header = TRUE, sep = ",")

distance <- function(data){
   aSquared <- ((lead(data$x)- data$x))^2
   bSquared <- ((lead(data$y)- data$y))^2
   zSquared <- ((lead(data$z)- data$z))^2
   cSquared <- sqrt(aSquared + bSquared + zSquared)
   
   distance_df <- data.frame(
     magnitude = cSquared,
     time = NA 
   )
  return (distance_df)
}

derivative <- function(dataframe){
  
  dt<-(lead(data$time)- data$time)
  
  dydx <- data.frame(magnitude = dataframe$magnitude/dt, time=lead(data$time))
  
  finalderiv<- na.omit(dydx)

  print(finalderiv)
}

distance_df <- distance(data)

velocity <- derivative(distance_df)
ggplot(data=velocity, aes(x=time, y=magnitude)) + geom_line() + geom_point()

#acceleration <- derivative(velocity)
#jerk <- derivative(acceleration)

#ggplot(data=acceleration, aes(x=time, y=magnitude)) + geom_line() + geom_point()
#ggplot(data=jerk, aes(x=time, y=magnitude)) + geom_line() + geom_point()
