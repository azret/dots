# Dots

An easy to use machine learning library written in C#


## Basic Concepts

A Dot(**·**) is a high level linear unit that produces a single scalar value **y**

y = **f**(x0, x1, ..., xn) = Ω(**Σ**(xj·βj) + **βc**)

It is updated according to the following

- βj(t) = βj(t-1) + **δ**·Xj
- βc(t) = βc(t-1) + **δ**

with **δ** for Ŷ (the desired output) as

**δj** = - (yj - ŷj) · dyj · **α** 

where 

- **α** : learning rate
- **dy** : partial derivative at Xj


## Identity Function
 
```csharp
Dot[] Ȳ = null;

for (int episode = 0; episode < 128 * 1024; episode++)
{
    var X̄ = new Dot[] 
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


## Xor

```csharp 
static Dots.Dot[][] X̄ = new Dots.Dot[][] 
{
    new Dots.Dot[] { -1, -1 },
    new Dots.Dot[] { -1, +1 },
    new Dots.Dot[] { +1, -1 },
    new Dots.Dot[] { +1, +1 },
};

static Dots.Dot[][] Ŷ = new Dots.Dot[][]
{
    new Dots.Dot[] { -1 },
    new Dots.Dot[] { +1 },
    new Dots.Dot[] { +1 },
    new Dots.Dot[] { -1 },
};         
```

One hidden layer with two units.

```csharp 
var H = new Dots.Dot[][]
{
	Dots.create(2, Dots.tanh().F) 
};
```

One output layer.

```csharp 
Dots.Dot[] Ȳ = null;
```

*Note* that the **Ȳ** vector is passed by **ref** and is elastic. It will be sized to the longest verctor in training set **Ŷ**.

```csharp
for (int episode = 0; episode < 128 * 1024; episode++)
{
	var m = random(X̄.Length);

    Dots.sgd(X̄[m], ref Ȳ, null, learn : Ŷ[m], rate: 0.1);    
}

Dots.compute(X̄, null, Ȳ);
```


## Resources

[Machine Learning (Stanford)](https://www.youtube.com/watch?v=UzxYlbK2c7E&list=PLJ_CMbwA6bT-n1W0mgOlYwccZ-j6gBXqE)

[Tensor Flow](https://www.tensorflow.org)
