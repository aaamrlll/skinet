let data: number;
let dataNumOrString: number | string;

// para dar strong typin a objetos usamos interfaces

interface ICar {
  color: string;
  model: string;
  age?: number;
  multiply?: (num1: number, num2: number) => number;
}

const multiplyMethod = (num1: number, num2: number): number => {
  return num1 * num2;
};

const car: ICar = {
  color: 'blue',
  model: 'bmw',
  multiply: multiplyMethod,
};

const car2: ICar = {
  color: 'red',
  model: 'bmw',
  age: 100,
};

let age = car2.age;

const ag2 = car.multiply(1, 2);

class Carclass implements ICar {
  color: string;
  model = '';
  age?: number;
  // para que solo pueda ser usado dentro de las sub Clases
protected length2 = 0;
// para que solo pueda ser usado por esta clase
 private length = 0;
  get _length(): number {
    return this.length;
  }
  set _length(value: number) {
    this.length = value * 2;
  }

  constructor(name: string) {
    this.model = name;
  }

  multiply?: (num1: number, num2: number) => number;

  divide(num1: number, num2: number): number {
    return num1 / num2;
  }
}

class Carclass2 extends Carclass {

  constructor() {
    super('yamaha');

    this.color = 'red';
    this.age = 100;
    this.length2 = 3;
    // hago override ala clase supper para el metodo divide
    super.divide = (): any => {};
  }
  // hago override ala clase supper para el metodo divide y paso a arrowfunction
  readonly divide = (num1: number, num2: number): number => {
    if (num2 > num1) {
      return super.divide(num1, num2);
    } else {
      return (num2 / num1) * 100;
    }
  }
}

const carclassobj = new Carclass2();
// con el set y el get cambia la prop privada
carclassobj._length = 3;

// da error por ser read only carclass.divide = () => {}
