module.exports = process.env.AUTH_COV
  ? require('./lib-cov/fizzbuzz.js')
  : require('./lib/fizzbuzz.js');
