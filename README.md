# VRTestBed 

Ok I set up the rotation scene. All the default params should be good! You just gotta write the code. I've already attached the script and given you access to both controllers and the cube already in the scene. 

When simulating the whole experiment from start to finish, this is the order you should go in:

1. Drag and Drop
2. Steering
3. Scaling
4. Pointing
5. Rotation

I set up the scene manager to run in this order. You can still run rotation or any other individually. 


BUGS TO SQUASH: 

DRAG AND DROP: Moving in certain directions makes sphere look oval. idk if that's an issue
STEERING: NA (PLIS VERIFY)
SCALING: Inconsistent 2 handed controls. I think the left hand doesn't always work. (That is controller with x button)
POINTING: I think the z value makes it so that a press down makes the sphere's change no matter what. idk for sure.
ROTATION: NOT STARTED

When you download all the files, can you simulate the experiment by starting with pointing task and then send me an email that it works as intended. Meaning you can do the experiment all the way to rotation? Some scenes require a double click for some reason. So definitely do that if there's a lag between scenes. 

Ok I think that is all!!

___________________________________________________________________________________________________

DATA ANALYZER: 

Things to Know: 

1. The program is in R
2. Acceleration & Jerk, although produce data frames come with a warning. I think this affects the ggplot
3. In order to use R, I recommend you download R Studio Desktop here: https://www.rstudio.com/products/rstudio/download/
4. I added the acceleration and jerk data frames & ggplots, but they are commented out for right now, due to the warning. I'm not sure I will be able to fix before I leave.
