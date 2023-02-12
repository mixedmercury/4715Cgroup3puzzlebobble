# 4715Cgroup3puzzlebobble

**VERY IMPORTANT FOR LEVELS TO FUNCTION**

Making Levels

1) Top row MUST be y = 5 and a row of 8 bubbles

2) Each bubble must be .875y under the row above it
  2a) Valid Y coordinates are: 5, 4.125, 3.25, 2.375, 1.5, 0.625, -0.25, -1.125, -2, -2.875, -3.75
 
3) Each row alternates between a maximum of 8 and 7 bubbles

4) All starting bubbles must somehow be touching the top of the frame, either directly or indirectly via touching other bubbles in a chain

5) The bubbles furthest to the side on rows of 8 will be x = -3.5 and x = 3.5
  5a) The possible X coordinates on rows of 8 are -3.5, -2.5, -1.5, -0.5, 0.5, 1.5, 2.5, 3.5
  
6) The bubbles furthest to the side on rows of 7 will be x = -3 and x = 3
  6a) The possible X coordinates on rows of 7 are -3, -2, -1, 0, 1, 2, 3
  
7) Each bubble must be on a coordinate using specifications provided above

8) The prefab for the bubbles has a public bool "Active". It is set to "True" in the prefab. Shot bubbles are set to Active = True by default. All starting bubbles MUST have Active set to "False" in the inspector.

9) These steps MUST be followed to minimize chances of issues with the code!
