'use strict';

exports.fizzbuzz = function(i) {
	return (i%3 ? '':'Fizz') + (i%5 ? '':'Buzz') || i;
};
