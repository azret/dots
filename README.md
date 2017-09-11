# Dots

An easy to use machine learning library written in C#

## Basic Concepts

A Dot(**·**) is a high level linear unit that produces a single scalar value **y**

y = **f**(x0, x1, ..., xn) = Ω(**Σ**(x0·β0 + x1·β1 + ... + xn·βn + **c**))

It is updated according to the following rule

βj(t) = βj(t+1) + **δ**·Xj

where **δ** is produced by the specified learing algorithm.

## Identity Function
 
 ```csharp
const int OUTPUTS = 7;

var Ȳ = Dots.create(OUTPUTS);

for (int episode = 0; episode < 128 * 1024; episode++)
{
    var X̄ = new Dot[OUTPUTS] 
	{
		random(),
		random(),
		random(),
		random(),
		random(),
		random(),
		random()
	};
    
    Dots.sgd(X̄, Ȳ, null, learn : X̄, rate: 0.1);    
}

Dots.compute(X̄, null, Ȳ);
```

## Resources

[Machine Learning (Stanford)](https://www.youtube.com/watch?v=UzxYlbK2c7E&list=PLJ_CMbwA6bT-n1W0mgOlYwccZ-j6gBXqE)

[Tensor Flow](https://www.tensorflow.org)
