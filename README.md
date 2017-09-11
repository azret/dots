# Dots

An easy to use machine learning library written in C#

A Dot(*·*) is basic linear computational unit that produces a single scalar value **y**

y = **f**(x0, x1, ..., xn) = Ω(**Σ**(x0*β0 + x1*β1 + ... + xn*βn + **c**))

## Identity Function
 
 ```csharp
const int OUTPUTS = 7;

var Ȳ = Dots.create(OUTPUTS);

for (int episode = 0; episode < 128 * 1024; episode++)
{
    var X̄ = GetInput(Dot.random(), max: OUTPUTS);
    
    Dots.sgd(X̄, Ȳ, null, learn : X̄, rate: 0.1);    
}

Dots.compute(X̄, null, Ȳ);
```

## Resources

[Machine Learning (Stanford)](https://www.youtube.com/watch?v=UzxYlbK2c7E&list=PLJ_CMbwA6bT-n1W0mgOlYwccZ-j6gBXqE)

[Tensor Flow](https://www.tensorflow.org)
