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

Next we'll create the code that we'll be able to 'evolve' our neural network. Evolution in this case will be pretty simple - we have only one gene (the weight) so
we won't use to parents to mix the genes. We're just going to mutate the best one to create some offspring.

First step is to create the initial pool of ANNs.

```c#
public class Pool
    {
        private int _numberOfSpecimens;
        private List<Specimen> _specimens = new List<Specimen>();
        private Random _rand = new Random();

        public Pool(int numberOfSpecimens)
        {
            _numberOfSpecimens = numberOfSpecimens;
            CreateInitialPool();
        }

        private void CreateInitialPool()
        {
            for (int i = 0; i < _numberOfSpecimens; i++)
            {
                var specimen = new Specimen();

                specimen.Brain = new NeuralNetwork();

                var randomWeight = (float)_rand.NextDouble() - 0.5f * 2f;
                new NeuralNetwork().SetWeights(randomWeight);

                _specimens.Add(specimen);
            }
        }
    }
```

The next step is to give the input to the each specimen and calulate its fitness - the closer the output is to the expected value the greater the fitness, with 1 being the 
best fitness we can get. We'll feed the whole simulation with a list of inputs and expected outputs and calculate the average error.

```c#
private float CalculateFitness(float[] input, float[] expectedOutput, NeuralNetwork neuralNetwork)
        {
            float error = 0;

            var numberOfInputs = input.Length;

            for (int i = 0; i < numberOfInputs; i++)
            {
                neuralNetwork.Process(input[i]);
                var actualOutput = neuralNetwork.GetOutput();
                error += Math.Abs(expectedOutput[i] - actualOutput);
            }

            return 1 - error / numberOfInputs;
        }
```

With the fitness of every NN in the pool calculated, we can sort them and us the best one to create the offspring.




