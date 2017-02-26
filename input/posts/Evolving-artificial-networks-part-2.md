Published: 2017-0x-xx
Title: Evolving artificial networks: Part 2
Lead: 
Author: Bartosz
---

    In the part 1 I've explained some theoretical basics for ANN. In part 2 I'll show you how to create and evolve a simple NN that is able to calculate
basic logical functions like AND and OR. 

I want to start with something really simple so it's easy to understand the whole elovling ANN business.

### The neuron, the layer, the network

In part one I've explained what is a mathematical model of neuron and what are neuron layers. Let's try to put that knowledge into a code. We'll start 
with the neuron it self. It's the most important part of an ANN after all.

I want to start with a very simple code and build upon it as we tackle more complex problems. The first problem thing we're gonna teach our network to solve is 
deciding if a number is less than or grater than 0. It's very simple problem, so simple actually,
that one neuron is enough to solve it. We'll be able to focus on understanding and building the code needed to evolve an ANN.

So, in our case we have only one input - a number to classify, and one output, with expected values of 0 if a number is less than 0 and 1 if it's greater than 0. We'll
also create the NeuralNetwork class to hold our neuron (and more neurons in the future) 

```c#
    public class Neuron
    {
        public float Weight;

        public float Output;

        public void Process(float input)
        {
            Output = input * Weight;
        }
    }

    public class NeuralNetwork
    {
        private readonly Neuron _outputNeuron;

        public NeuralNetwork()
        {
            _outputNeuron = new Neuron();
        }

        public void Process(float input)
        {
            _outputNeuron.Process(input);
        }

        public float GetOutput()
        {
            return _outputNeuron.Output;
        }

        public void SetWeights(float weight)
        {
            _outputNeuron.Weight = weight;
        }

        public float GetWeights()
        {
            return _outputNeuron.Weight;
        }
    }
```

So the gist of our ANN is the Process() method of the neuron. As you see it takes the input and multiplies it by the weight it holds for that input. Right know 
there is only one input so we need only one weight. We could is the neuron itselft for our computations but I've wrapped it up into a NeuralNetwork class so it's easier
to extend out ANN in the future. 

### Coded to evolve



