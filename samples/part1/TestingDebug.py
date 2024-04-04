#! python3

import Rhino as R

class MyClass:
    def __init__(self):
        self.some_value = 12
        self.point = R.Geometry.Point3d.Origin

    def SomeMethod(self):
        print(self.some_value)


def func_call_test(a, b):
    def nested_func_call_test(c):
        d = MyClass()
        print(d, c)

    print(a, b)
    nested_func_call_test(a + b)


func_call_test(10, 10)

myList = [1, 2, 3]
print(3)