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


## Identity Function (Linear Regression)
 
The following example learns the ȳ = f(x̄) = x̄ function.

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
    
    Dots.sgd(X̄, ref Ȳ, null, learn : X̄, rate: 0.1);    
}

Dots.compute(X̄, null, Ȳ);
```


## Xor Function (Multi-layer Feed Forward Network)

Training examples

```csharp 
static Dots.Dot[][] X̄ = new Dots.Dot[][] 
{
    new Dots.Dot[] { -1, -1 },
    new Dots.Dot[] { -1, +1 },
    new Dots.Dot[] { +1, -1 },
    new Dots.Dot[] { +1, +1 },
};
```

Target values

```csharp 
static Dots.Dot[][] Ŷ = new Dots.Dot[][]
{
    new Dots.Dot[] { -1 },
    new Dots.Dot[] { +1 },
    new Dots.Dot[] { +1 },
    new Dots.Dot[] { -1 },
};         
```

[-1, 1]

```csharp 
var tanh = Dots.tanh().F;
```

[0, 1]

```csharp 
var sigmoid = Dots.sigmoid().F;
```

One hidden layer with two units

```csharp 
var H = new Dots.Dot[][]
{
    Dots.create(2, tanh) 
};
```

One output layer

```csharp 
Dots.Dot[] Ȳ = null;
```

*Note* that the **Ȳ** vector is passed by **ref** and will be sized to the longest vector in training set **Ŷ**

```csharp
for (int episode = 0; episode < 128 * 1024; episode++)
{
    var M = random(X̄.Length);

    Dots.sgd(X̄[M], ref Ȳ, H, learn : Ŷ[M], rate: 0.1, tanh);    
}

Dots.compute(X̄, H, Ȳ);
```

After training

```csharp
[-1, -1] = [-0.999999999999999]
[-1, +1] = [+0.999999999999999]
[+1, -1] = [+0.999999999999999]
[+1, +1] = [-0.999999999999999]
```

## Resources

[Machine Learning (Stanford)](https://www.youtube.com/watch?v=UzxYlbK2c7E&list=PLJ_CMbwA6bT-n1W0mgOlYwccZ-j6gBXqE)

[MIT 6.034](https://ocw.mit.edu/courses/electrical-engineering-and-computer-science/6-034-artificial-intelligence-fall-2010/lecture-videos)

[Tensor Flow](https://www.tensorflow.org)
