redirectToCheckout = function (sessionId) {
	var stripe = Stripe('pk_test_51IAeM8LCudpQrkG8XPOPzKW6DQz5mFY9rBnCMkVJykY7rOZvrtNwoN1QJ7NkLvj7fAlgkAMeexx8ubOVORJbMj7X00fyhM5fbV');
	stripe.redirectToCheckout({
		sessionId: sessionId
	});
};