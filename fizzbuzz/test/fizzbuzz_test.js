'use strict';

var should = require('should');
var FizzBuzz = require('../lib/fizzbuzz.js').FizzBuzz;

describe('FizzBuzz basic operations', function() {

	var fb = new FizzBuzz();

	it('should print Fizz when divisible by 3', function() {
		fb.evaluate(3).should.eql('Fizz');
		fb.evaluate(9).should.eql('Fizz');
	});

	it('should print Fizz when divisible by 5', function() {
		fb.evaluate(5).should.eql('Buzz');
		fb.evaluate(20).should.eql('Buzz');
	});

	it('should print FizzBuzz divisible by 3 and 5', function() {
		fb.evaluate(15).should.eql('FizzBuzz');
		fb.evaluate(300).should.eql('FizzBuzz');
	});

	it('should print number when divisible by 3 or 5', function() {
		fb.evaluate(1).should.eql('1');
		fb.evaluate(7).should.eql('7');
	});
});

describe('FizzBuzz loop to 30', function() {
	var fb = new FizzBuzz();

	it('should print correct numbers when looping to 30', function () {
		var arr = ['FizzBuzz', 1, 2, 'Fizz', 4, 'Buzz', 'Fizz', 7, 8, 'Fizz', 'Buzz', 11, 'Fizz', 13, 14, 'FizzBuzz', 16, 17, 'Fizz', 19, 'Buzz', 'Fizz', 22, 23, 'Fizz', 'Buzz', 26, 'Fizz', 28, 29, 'FizzBuzz'];	

		for(var i = 0; i < 30; i++) {
			fb.evaluate(i).should.eql(arr[i]);		
		}
	});
});
