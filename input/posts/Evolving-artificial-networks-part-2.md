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
        public double Weight;

        public double Output;

        public void Process(double input)
        {
            var sum = input * Weight;
            Output = Sigmoid(sum);
        }

        private double Sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

    }

    public class NeuralNetwork
    {
        private readonly Neuron _outputNeuron;
        private double[] _weights;

        public NeuralNetwork()
        {
            _outputNeuron = new Neuron();
        }

        public void Process(double input)
        {
            _outputNeuron.Process(input);
        }

        public double GetOutput()
        {
            return _outputNeuron.Output;
        }

        public void SetWeights(double[] weights)
        {
            _weights = weights;
            _outputNeuron.Weight = weights[0];
        }

        public double[] GetWeights()
        {
            return _weights;
        }
    }
```

So the gist of our ANN is the Process() method of the neuron. As you see it takes the input and multiplies it by the weight it holds for that input. Right know 
there is only one input so we need only one weight. Then we pass the sum through the sigmoid function to squash it nicely between 0 and 1. 
We could use the neuron itself as our first ANN but I've wrapped it up into a NeuralNetwork class so it's easier
to work with it in the future. 

### Coded to evolve

Next we'll create the code that we'll be able to 'evolve' our neural network. Evolution in this case will be pretty simple - we have only one gene (the weight) so
we won't use to parents to mix the genes. We're just going to mutate the best one to create some offspring.

First step is to create the initial pool of ANNs.

```c#
public class Pool
    {
        private readonly int _numberOfSpecimens;
        private readonly List<Specimen> _specimens = new List<Specimen>();
        private readonly List<SpecimenFitness> _fitness = new List<SpecimenFitness>();
        private readonly Random _rand = new Random();
        private double _mutationRate = 0.5f;

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

                var randomWeight = ((double)_rand.NextDouble() - 0.5f) * 2f;
                specimen.Brain.SetWeights(new double[] { randomWeight });

                _specimens.Add(specimen);
            }
        }
    }
```

The connection (synapse) can be wyzwalajace or zmniejszajace wyzwalanie, that's why I modified the random number to fit between -1 and 1. 
The next step is to pass the input to the each specimen and calulate its fitness - the closer the output is to the expected value the greater the fitness, with 1 being the 
best fitness we can get. We'll feed the whole simulation with a list of inputs and expected outputs and calculate the average error.

```c#
public class Pool
    {
        ...
private readonly List<SpecimenFitness> _fitness = new List<SpecimenFitness>();

public void CalculateFitness(double[] input, double[] expectedOutput)
        {
            _fitness.Clear();
            foreach (var specimen in _specimens)
            {
                var fitness = CalculateFitness(input, expectedOutput, specimen.Brain);
                var specimentFitness = new SpecimenFitness(specimen, fitness);
                _fitness.Add(specimentFitness);
            }
        }

private double CalculateFitness(double[] input, double[] expectedOutput, NeuralNetwork neuralNetwork)
        {
            double error = 0;

            var numberOfInputs = input.Length;

            for (int i = 0; i < numberOfInputs; i++)
            {
                neuralNetwork.Process(input[i]);
                var actualOutput = neuralNetwork.GetOutput();
                error += Math.Abs(expectedOutput[i] - actualOutput);
            }

            return 1 - (error / numberOfInputs);
        }
        ...
```

With the fitness of every NN in the pool calculated, we'll be able sort them and use the best one to create the offspring. We have only one gene now (one weight),
so we won't use 2 parents and create the offspring by mutating the one specimen. Each child will get a new weight that is randomily incresed or decreased compared to 
the parent. 

```c#
public class Pool
    {
        ...

        private double _mutationRate = 0.5f;

         public void CreateChildPool(Specimen parent)
        {
            for (int i = 0; i < _numberOfSpecimens; i++)
            {
                var parentWeights = parent.Brain.GetWeights();
                var mutated = Mutate(parentWeights);
                _specimens[i].Brain.SetWeights(mutated);
            }
        }

        private double[] Mutate(double[] parentWeights)
        {
            var change = ((double)_rand.NextDouble() - 0.5f) * 2f;
            var mutated = new double[parentWeights.Length];

            for (int i = 0; i < parentWeights.Length; i++)
                mutated[i] = parentWeights[i] + change * _mutationRate * parentWeights[i];

            return mutated;
        }
        ...
```

Having all of the basic blocks together, we can create the main evolution loop:


```c#
public class EvolutionSimulator
    {
        private readonly Pool _pool;

        private double[] _inputs;
        private double[] _expectedOutputs;

        private int _numberOfSpecimens = 100;

        public EvolutionSimulator()
        {
            _pool = new Pool(_numberOfSpecimens);

            _inputs = new double[] { -5f, -4f, -3.24f, -1.4f, -0.5f, -0.1f, 0.1f, 0.4f, 1.4f, 2.5f, 4.5f, 5.5f, 7.5f };
            _expectedOutputs = new double[_inputs.Length];
            for (int i = 0; i < _inputs.Length; i++)
            {
                _expectedOutputs[i] = _inputs[i] > 0 ? 1 : 0;
            }
        }

        public void Simulate()
        {
            int generation = 0;
            double fitness = 0;
            SpecimenFitness specimenFitness = null;

            while (fitness < 0.9999f && generation < 1000)
            {
                _pool.CalculateFitness(_inputs, _expectedOutputs);
                specimenFitness = _pool.GetBestSpecimen();
                fitness = specimenFitness.Fitness;
                _pool.CreateChildPool(specimenFitness.Specimen);

                Console.WriteLine($"Generation {generation}, best fitness: {specimenFitness.Fitness}");
                generation++;
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Number (hit return to exit):");
                    string number = Console.ReadLine();
                    if (string.IsNullOrEmpty(number))
                        break;
                    double parsed = double.Parse(number);

                    specimenFitness.Specimen.Brain.Process(parsed);
                    Console.WriteLine(specimenFitness.Specimen.Brain.GetOutput());
                }
                catch (Exception e)
                {
                    Console.WriteLine("err");
                }
            }
        }
    }
```

I've defined two stop values here, so we don't end up with neverending loop - we'll stop the simulation if the fitness is higher than 0,9999 or when we hit generation 1000.
In each loop we calculate the fitness, choose the best specimen and use it to generate new pool. At the end of the simulate method we'll add a small loop so we can 
test the best speciman evolved by the algorithm.


```c#
public class EvolutionSimulator
    {
        ...
        public void Simulate()
        {
            ...
            while (true)
            {
                try
                {
                    Console.WriteLine("Number (enter nothing to exit):");
                    string number = Console.ReadLine();
                    if (string.IsNullOrEmpty(number))
                        break;
                    double parsed = double.Parse(number);

                    specimenFitness.Specimen.Brain.Process(parsed);
                    Console.WriteLine(specimenFitness.Specimen.Brain.GetOutput());
                }
                catch (Exception e)
                {
                    Console.WriteLine("err");
                }
            }
        }
    }
```

The simulation ends pretty quickly - I get a good enough specimen around 10th generation. 

### Biased networks

One thing that's missing from the code is the Bias. Imagine you want the network to be able to discern if the number is greater or less than 4. 
Let's modify our test data then:

```c#

_inputs = new double[] { -5f, -4f, -3.24f, -1.4f, -0.5f, -0.1f, 0.1f, 0.4f, 1.4f, 2.5f, 4.5f, 5.5f, 7.5f };
_expectedOutputs = new double[_inputs.Length];
for (int i = 0; i < _inputs.Length; i++)
{
     _expectedOutputs[i] = _inputs[i] > 4 ? 1 : 0;
}

```

The simulation can't create a specimen that works as expected at all! The best fitness I got is ~0.7 and it gives me the wrong outputs for numbers like 0, 2, 2.5 etc.
This is where bias comes in. Without it, each neuron of our network can produce a meaningfull output only for the data close to 0 (because sigmoid function). 
The bias allows the neuron to shift the activation function left or right so it can operate on different range of inputs. Let's modify the code to include 
the bias in our calculation and mutation:

```c#
public class Neuron
    {
...
        public double Bias;

        public void Process(double input)
        {
            var sum = input * Weight + Bias; // We add the bias to shift the function
            Output = Sigmoid(sum);
        }
...

public class NeuralNetwork
    {
...
        public void SetWeights(double[] weights)
        {
            _weights = weights;
            _outputNeuron.Weight = weights[0];
            _outputNeuron.Bias = weights[weights.Length - 1];   // The last weight in the weight array will be always a bias
        }
...
    }

public class Pool
    {
        ...
private void CreateInitialPool()
        {
            for (int i = 0; i < _numberOfSpecimens; i++)
            {
                var specimen = new Specimen();

                specimen.Brain = new NeuralNetwork();

                var randomWeight = ((double)_rand.NextDouble() - 0.5f) * 2f;
                var randomBias = ((double)_rand.NextDouble() - 0.5f) * 2f;

                specimen.Brain.SetWeights(new double[] {randomWeight, randomBias });

                _specimens.Add(specimen);
            }
        }
        ...
    }
```

With the bias in place, our algorithm is able to evolve a specimen that solves our problem. 

In the next part we're going to increase complexity by adding more inputs and tackle the evolutionary dead end problem. 