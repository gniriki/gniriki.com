Published: 2018-05-17
Title: ML in practice - the Boston Housing problem
Lead: let's continue applying our new ML knowledge by solving an actual problem
Author: Bartosz
Tags:
  - Machine Learning
  - Coursera
---

[Last time](/posts/Learning-ML-from-theory-to-practice) I set up the environment and solved a quick and easy example to test the waters. It's time to tackle a harder problem. Fortunately for me ```scikit-learn``` comes with a few build-in datasets that I can play with. I want to practice what I've learned at the beginning of the course, so I will try to solve something with Linear Regression first. 

I looked at the datasets present in the library and decided that [Boston hource pricing](http://scikit-learn.org/stable/modules/generated/sklearn.datasets.load_boston.html#sklearn.datasets.load_boston) might be a good place to start. 

### Quick and dirty

One of the things that prof. Ng mentioned a few times during the course was getting some dirty solution up and running quickly, before trying anything else. With a working solution, we can try and find areas we can focus on, instead of wasting time doing unnecessary stuff. Let's try to quickly fit a simple linear regression and see how it goes. First, I'll split the data between train and tests sets and than I'll calculate scores for both. I also pass in the ```random_state``` argument, so the split is performed in the same way every time. 

```
import sklearn.datasets as ds
import sklearn.model_selection as md

data = ds.load_boston();

X_train, X_test, y_train, y_test = md.train_test_split(
     data.data, data.target, random_state=1)

from sklearn.linear_model import LinearRegression

reg =  LinearRegression();

reg.fit(X_train, y_train)

print(reg.score(X_train, y_train))
print(reg.score(X_test, y_test))
```

```
Train score: 0.7167286808673383
Test score: 0.7790257749137512
```

Now we can work on making the score better!

### Learning curves

One of the way to 'debug' the algorithm that I learned was plotting learning curves. When I was doing the homework, I had to save J (the error) during each learning step. Fortunately, ```scikit``` comes with a neat function that does everything for you - [learning_curve](http://scikit-learn.org/stable/auto_examples/model_selection/plot_learning_curve.html). The function will divide your train set randomly into a few sets and compute how the score changes with each step. By default it divides your data to 5 steps/sets, but I increased it to 10 by passing ```train_sizes``` argument, so it looks smoother.

```
from sklearn.model_selection import learning_curve
from matplotlib import pyplot
import numpy as np

train_sizes, train_scores, cv_scores = learning_curve(reg, X_train, y_train, train_sizes = np.linspace(.1, 1.0, 10))

train_scores_mean = np.mean(train_scores, axis=1)
cv_scores_mean = np.mean(cv_scores, axis=1)

pyplot.ylim((-1, 1.0))

pyplot.plot(train_sizes, train_scores_mean, 'ro-', label="Train score")
pyplot.plot(train_sizes, cv_scores_mean, 'go-', label="CV score")
pyplot.legend()
```

![Learning curves](/content/posts/learning-ml-part-2/learning-curves-1.png "Learning curves")

This plot tells me two things:
1. Train and CV score converge so it's not a high variance problem
2. The score of both is pretty low, so it might be a high bias problem

High bias problem usually stems from model not fitting the data well.

### Polynomial features

One of the simplest things we can do to fix our problem is adding some polynomial features. This will allow the model to fit our data better. ```Scikit``` comes with a neat function to add the features for us - [PolynomialFeatures](http://scikit-learn.org/stable/modules/generated/sklearn.preprocessing.PolynomialFeatures.html). Let's modify our data preparation step:

```
...
import sklearn.model_selection as md
from sklearn.preprocessing import PolynomialFeatures

data = ds.load_boston();

data.data = PolynomialFeatures(degree=3).fit_transform(data.data)

X_train, X_test, y_train, y_test = md.train_test_split(
     data.data, data.target)
...
```

And run the code again:

```
Train score: 1.0
Test score: -690.9819870039847
``` 

![Overfitting](/content/posts/learning-ml-part-2/learning-curves-2.png "Overfitting")

The score and learning curve tells me that we moved from having high bias problem to having high variance one. The model fits the training data so well that it fails miserably when it comes to the test data. 

### Normalization

To fix our current problem we can use regularization, but for it to work properly, we need to scale the features first. Regularization penalizes each coefficient with the same "force", so having features in the different ranges or order of magnitudes will cause problems. I'll use [StandardScaler](http://scikit-learn.org/stable/modules/generated/sklearn.preprocessing.StandardScaler.html) which will remove the mean and normalize feature values. Will fit the scaler to the training data and then use it to transform the test data as well.

```
...
X_train, X_test, y_train, y_test = md.train_test_split(
     data.data, data.target)

from sklearn.preprocessing import StandardScaler

scaler = StandardScaler()
X_train = scaler.fit_transform(X_train)
X_test = scaler.transform(X_test)

from sklearn.linear_model import LinearRegression
...
```

### Regularization

Now that our data is prepared properly, we can add regularization. To add it to our code, we need to switch from LinearRegression to [Ridge](http://scikit-learn.org/stable/modules/generated/sklearn.linear_model.Ridge.html) regressor. We need to provide it with ```alpha``` parameter which will control how much the coefficients are penalized. Fortunately, instead of guessing it by ourselves (and wasting time testing different values), the library can do it for us. [RidgeCV](http://scikit-learn.org/stable/modules/generated/sklearn.linear_model.RidgeCV.html) is a version of Ridge regressor that will guess it for us. All we need to do is provide it with a few alpha values to try:

```
...
X_test = scaler.transform(X_test)

from sklearn.linear_model import RidgeCV

alphas=[0.1,0.3, 1,3, 10,30,100,300, 1000]
reg = RidgeCV(alphas)

reg.fit(X_train, y_train)

print("Train score: " + str(reg.score(X_train, y_train)))
print("Test score: " + str(reg.score(X_test, y_test)))
print("Selected alpha: " + str(reg.alpha_))

from sklearn.model_selection import learning_curve
...
``` 

Result:

![Niceee](/content/posts/learning-ml-part-2/learning-curves-3.png "Niceee")
```
Train score: 0.9347137854004398
Test score: 0.9294740071777031
```

I think those results are pretty good. 

Another thing that would help with our overfitting problem would be gathering more data. It's not possible in this case, but can be really helpful in real world.

Cheers,
Bartosz