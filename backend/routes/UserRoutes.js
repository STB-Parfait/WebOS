const express = require('express');
const router = express.Router();
const userController = require('../controllers/UserController.js');

router.route('/')
    .post(userController.createUser)
    .get(userController.getUsers);

router.route('/checkemail')
    .post(userController.checkEmails)

router.route('/:id')
    .get(userController.getUserById)
    .delete(userController.deleteUser);

module.exports = router;