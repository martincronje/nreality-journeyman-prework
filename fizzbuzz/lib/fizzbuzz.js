'use strict';

exports.FizzBuzz = function() {
	this.evaluate = function(i) {
		return (i%3 ? '':'Fizz') + (i%5 ? '':'Buzz') || i;
	};
};
