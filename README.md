# Boids
This is an experiment project on Boids and its applications in game development. Boids or bird-like objects is an artificial program, developed by [Craig W. Reynolds](https://en.wikipedia.org/wiki/Craig_Reynolds_(computer_graphics)) in 1986, which simulates the flocking behavior of birds.
In this project I have implemented a Boids simulation in Unity, using a set of simple rules (seperation,alignment,cohesion) as explained by [Craig W. Reynolds](https://en.wikipedia.org/wiki/Craig_Reynolds_(computer_graphics)) in his artile [Flocks, Herds, and Schools:
A Distributed Behavioral Model](http://www.cs.toronto.edu/~dt/siggraph97-course/cwr87/)

## Simulation
First I atarted with a simple 2D simulation of Boids in unity.
### Setting up the simulation environment
For the environment I have used a rectangular area with specified width and height that you can control as a simulation settings parameter and with wrap around walls just like in classic asteroid game, then I am instantiating the Boids game object in this play area with a random position and a random velocity.

https://user-images.githubusercontent.com/54682356/172447702-35cc2379-425f-4552-98b7-f22520e5e79c.mp4

