# Dots

An easy to use machine learning library written in C#


## Basic Concepts

A Dot(**·**) is a high level linear unit that produces a single scalar value **y**

y = **f**(x0, x1, ... , xn) = Ω(**Σ**(xj·βj) + **βc**)

It is updated according to the following:

- βj(t) = βj(t-1) + **δ**·Xj
- βc(t) = βc(t-1) + **δ**

with **δ** for Ŷ (the desired output) as

**δj** = - (yj - ŷj) · δyj · **α** 

where 

- **α** : learning rate
- **δy** : partial derivative at Xj

minimizing the cost function

1/2·**Σ**(yj - ŷj)²


## Developing Intuition

Let's start with one-dimensional input vectors.

A single Dot(**·**) then is just a straight line

y=**f**(x)=a·**x**+b

at some angle **a** and height **b**.

The task of a learning algorithm is to find coefficients **a** and **b** such that the desired **y** is produced.

In other words, we are looking for a line that maps **x** into **y**.

Likewise, for higher dimensions the task is to find a higher dimential object. A hyperplane for n-dimensions.

ȳ = **f**(x̄) = βᵀx̄ + βc  

e.g. A one-dimensional identity function, or y = **f**(x) = x = 1.0·x + 0.0

![y=f(x)=a·x](/Line.png?raw=true "y=f(x)=a·x+b")


## Identity Function (Linear Regression)
 
The following example learns a multi-dimensional identity function

**ƒ**(X̄) = X̄

```csharp
var ℳ = new Dot[][]
{
    Dots.create(count: SIZE),
};

ℳ.connect(X: SIZE, randomize : true);

for (var i = 0; i < EPISODES; i++) {
    Dot[] T = Dots.random(SIZE);

    ℳ.compute(T);

    ℳ.sgd(T, learningRate: 0.1, momentum: 0.9);
}

var X = Dots.random(INPUTS);
var Y = ℳ.compute(X);

Dots.print(X, "n4", Console.Out);
Dots.print(Y, "n4", Console.Out);
```
