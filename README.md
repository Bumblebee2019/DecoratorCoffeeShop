## Decorator Coffee Shop

Decorator is a structural design pattern that allows us add new behaviors to objects by wrapping them inside special wrapper objects that contain the desired behavior. What is a better way to learn a design pattern than to use it for creating a delicious drink? 

Why use a design pattern for such a simple program?
If we only served regular tea or coffee in standard sized cups, using the decorator would have been unnecessary. However, offering 3 cup sizes, several milk types, and condiment options, the amount of subclasses we would need to create to cover every possible combination would be huge. To simplify the program, we add a decorator, which inherits from the interface (BasicDrink, in our case). And the concreate decorators (MilkDecorator, ToppingDecorator, WhippedCreamDecorator) all inherit from that base decorator.

Here is a UML diagram for my coffee shop:

The user is first greeted with the menu:


The user is promted to customize his or her drink: 


