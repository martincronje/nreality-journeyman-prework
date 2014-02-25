'use strict';

var should = require('should');
var fizzbuzz = require('../').fizzbuzz;

console.log(fizzbuzz);

describe('FizzBuzz basic operations', function() {

	it('should print Fizz when divisible by 3', function() {
		fizzbuzz(3).should.eql('Fizz');
		fizzbuzz(9).should.eql('Fizz');
	});

	it('should print Fizz when divisible by 5', function() {
		fizzbuzz(5).should.eql('Buzz');
		fizzbuzz(20).should.eql('Buzz');
	});

	it('should print FizzBuzz divisible by 3 and 5', function() {
		fizzbuzz(15).should.eql('FizzBuzz');
		fizzbuzz(300).should.eql('FizzBuzz');
	});

	it('should print number when divisible by 3 or 5', function() {
		fizzbuzz(1).should.eql('1');
		fizzbuzz(7).should.eql('7');
	});
});

describe('FizzBuzz loop to 30', function() {
	it('should print correct numbers when looping to 30', function () {
		var arr = ['FizzBuzz', 1, 2, 'Fizz', 4, 'Buzz', 'Fizz', 7, 8, 'Fizz', 'Buzz', 11, 'Fizz', 13, 14, 'FizzBuzz', 16, 17, 'Fizz', 19, 'Buzz', 'Fizz', 22, 23, 'Fizz', 'Buzz', 26, 'Fizz', 28, 29, 'FizzBuzz'];	

		for(var i = 0; i < 30; i++) {
			fizzbuzz(i).should.eql(arr[i]);		
		}
	});
});
