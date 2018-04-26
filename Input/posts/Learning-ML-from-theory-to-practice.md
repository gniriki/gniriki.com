Published: 2018-04-26
Title: Learning ML - From theory to practice
Lead: I just finished a great course on Coursera, "Machine Learning", and it's time to put this new knowledge to use!
Author: Bartosz
Tags:
  - Machine Learning
  - Coursera
---

I got interested in ML some time ago and was looking for some comprehensive introduction. The [Machine Learning](https://www.coursera.org/learn/machine-learning/) course taught by prof. Andrew Ng seemed like a really good place to start. The course really teach you the basics (from implementing linear regression to neural network backpropagation) and, while you probably don't need to know those internals to use some ML libraries, it really helps you understand what's going on. Especially why things may go wrong and how to fix them.

There is a lot of theory and knowledge there along with a few practical tasks (homeworks) to do. You can quickly learn a lot from it, but, as with everything, if you don't use it, you lose it. That's why I decided to use my newfound knowledge to
resolve at least a few problems to get a better hang of it.

### The tools

The course tasks are done in Octave, but it seems that Python is one of the most popular languages when it comes to machine learning. I don't know much about it (my background is C# and some JavaScript), but hopefully it's as easy as they say! I was following some news about ML/AI in recent months and it seems that there are at least a few main libraries one can choose from. As I know next to nothing about ML in Python, trying to choose the 'best' one for me right now feels like a waste of time. I just decided to pick [scikit-learn](http://scikit-learn.org/stable/index.html) and roll with it.

### Setting up the environment

Scikit-learn [installation manual](http://scikit-learn.org/stable/install.html) says that it's easier to setup everything with a third-party distribution than by installing it manually yourself. I like easy so I went with [Anaconda](https://www.anaconda.com/download/). It seems to have everything one might need fo ML - plotting libs, math libs, data libs, etc.

### Toy excercise

Let's start with something simple. I've seen a few examples of scikit usage here and there and it seems that there is build in dataset library. After a quick look at the manual, I've found something that will help me to get started - [make_regression](http://scikit-learn.org/stable/modules/generated/sklearn.datasets.make_regression.html#sklearn.datasets.make_regression) function that can be used to generate some simple data.

A quick look at the arguments and:

 ```
 import sklearn.datasets as datasets

 X, y = datasets.make_regression(n_features=1)
 ```

It's best to plot it to make sure we got something usable. Mathplotlib seems to be the library for plotting. A quick look at the manual to find how to plot 2D data and:

```
import matplotlib.pyplot as pyplot
pyplot.plot(X, y, 'o')
```

![Oops](/content/posts/learning-ml-part1/linear-data.png "That's too easy...")

That's a bit too obvious. Fortunately, ```make_regression``` has a parameter called ```noise```. A few tests to choose a value and:
```
X, y = datasets.make_regression(n_features=1, noise=10)
pyplot.plot(X, y, 'o')
```
![That's better](/content/posts/learning-ml-part1/better-data.png "That's better")

### Linear regression

Let's divide the data into train and test sets:
```
X_train = X[0:80]
X_test = X[81:100]

y_train = y[0:80]
y_test = y[81:100]
```

Now that we have some data, let's try to do some actual ML - a linear regression. Quick look at the manual tells me that we can use [LinearRegression](http://scikit-learn.org/stable/modules/linear_model.html#ordinary-least-squares):

```
from sklearn import linear_model
reg = linear_model.LinearRegression()
reg.fit(X_train, y_train)
reg.score(X_train, y_train)
reg.score(X_test, y_test)
```
Nice! I got 91% for the train set and 85% for the test set. Last, but not least, let's plot everything:

```
pyplot.plot(X, y, 'o', X, reg.coef_ * X)
```
![It worked!](/content/posts/learning-ml-part1/linear-fit.png "It worked!")

### That was easy!

Ok, so it was really simple example, but it was great for starters. I knew (or rather assumed) that ```scikit-learn``` will have some kind of function/object for training linear regression but I've also learned that it comes with some nice predefined datasets and helpers to create new data and that ```matplotlib``` can be used for quick plotting. Those libraries were really easy to use and I could quickly find how to use them in the manuals. Writing this text took me much longer than doing the whole thing!
I also liked how transferable my knowledge from the course/Octave have been. Same style for returning multiple results (```X,y = ...```) and same arguments for plot function for example.

Next up - playing with sample datasets that come with ```scikit-learn```.

Cheers!



