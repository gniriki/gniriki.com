Published: 2017-0x-xx
Title: Evolving artificial networks: Part 1
Lead: 
Author: Bartosz
---

    Some time ago I had an idea for a game involving small bots and while searching for similar things online I came upon this video: https://www.youtube.com/watch?v=1iamM0SuPto
I'm not sure if using actuall ANN is a viable game idea, but it got me interested in evolving NN enought to try and code it myself. 
I had to look into a lot of different sources to figure it out, so I created this series in hope to explain all of the basics in one place. 

### Some theory behind everything

So what are artificial neural networks anyway? 

Let's start with the smallest, but the most important part of the ANN: The neuron.

You've probably seen this image, the sketch of biological neuron.

# Image of bio neuron

It's function is more complicated that we thought before, and we still learn new stuff about it, but the simplest abstraction of it is as follows:

# image of neuron math model

The neuron has inputs, that are connected to the outputs of another neurons and depending on the inputs roduce some output. In computer models it usually means
that each input is multiplied by it's weight, we sum them and pass through some function - sigmoid being one the favourits. 

### Why are they so interesting? 

One neuron in and of itself isn't that interesting. But if you connect a few of them, creating an Artificial Neural Network, the magic happens. 
Just by using those simple calculations, a intelligence of sort can emerge. Depending on the complexity it can be used to recognize images, predict words or 
control things better that we could program it to do. Some of the tasks it can do aren't really programmable at all - how to you write an algorithm to recognize a
picture of a cat for example?

### Learing the behaviour

Creating the network structure isn't enought. If you just give the neurons some random weigths, all you'll get will be some random outputs. 
For it to work you have to 'teach' it the correct behaviour somehow.
The teaching way that powers most of the ANNs today is called back propagation. You basically give the ANN an input (an image for example), calculate how much off
it is from the desired output and then calculate the error each neuron has, staring with the output layer (thus back propagation). Using those errors you can update 
the weight in each neuron to improve the output of the whole network. Do that enought of times and the ANN will be able to do what you expect it to do. 

### Evolving the behaviour

Another way to get the behaviour we want is to evolve it. This basically means creating a set of networks, called specimens, choosing the best ones which will become 'parents' and then using them to create
a new set of ANNs, the children. Rinse and repeat till we get the expected behaviour. This method is a lot slower than backpropagation, but a lot better suited for tasks that don't have the end result clearly defined,
like creating the best bot. It's also a lot more interesing to watch :)

### Evolution

The evolution of ANNs is based on simplified evolution of real life organisms. In real world the natural selection is the mechanism???

We need to decide on the way to measure the fitness of our ANN first. For simple ones, it can be a mathematical error of the output. For complex ones, like bots, lifespan is a good way to measure fitness.
After grading each one of out specimens we can choose a few good ones that will be use to create the offspring. Similarily to the real world, we will mix the genes of the parents and introduce some mutations
which will have a chance of producing a better behaviour. For now I'll be using ANN with a static structure, manipulating the weights only. That kind of ANNs are simpler to work with but usually can't produce the
complex behaviour expected from bots. 
Further in the series I'll explain a way to evolve the stucture itself too (https://en.wikipedia.org/wiki/Neuroevolution_of_augmenting_topologies) and I'll use that to control the bots.

